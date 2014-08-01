using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.IO;
using Client.LocalDbAccess;
using Client.LocalFileSysAccess;
using System.Windows.Forms;

namespace Client.PollFunction
{
    public class Poll
    {
        private string clientSynFolderPath;
        private string sasUri;

        public Poll(string sasUri)
        {
            //MessageBox.Show("Calling Poll constructor", "Client");
            this.clientSynFolderPath = localSyncFolderPath();
            this.sasUri = sasUri;
            scanAllFiles();
            ListWithContainerUri();
        }

        private bool ListWithContainerUri()
        {
            try
            {
                
                CloudBlobContainer container = new CloudBlobContainer(new Uri(sasUri));
                string blobPrefix = null;
                bool useFlatBlobListing = true;
                var blobs = container.ListBlobs(blobPrefix, useFlatBlobListing, BlobListingDetails.None);
                foreach (IListBlobItem item in blobs)
                {
                    if (item is CloudBlobDirectory)
                    {
                        Console.WriteLine("this is folder");
                        Console.WriteLine(item.Uri);
                    }
                    else
                    {
                        CloudBlockBlob file = (CloudBlockBlob)item;
                        file.FetchAttributes();
                        string fileFullPath = clientSynFolderPath + @"\"+ file.Metadata["filePath"];
                        DateTime blobDataTime = new DateTime();
                        blobDataTime = DateTime.ParseExact(file.Metadata["timestamp"], "MM-dd-yyyy HH:mm:ss",
                                                            null);
                        if(File.Exists(fileFullPath))
                        {
                            getFileAttributes fileAttributes = new getFileAttributes(fileFullPath);
                            /*
                            DateTime blobDataTime = new DateTime();
                            blobDataTime = DateTime.ParseExact(file.Metadata["timestamp"], "MM-dd-yyyy HH:mm:ss",
                                                                null);
                             */ 
                            // if need to poll
                            if (DateTime.Compare(blobDataTime, fileAttributes.lastModified.ToUniversalTime()) > 0)
                            {
                                if (fileAttributes.md5Value != file.Metadata["hashValue"])
                                {
                                    pollFile(file, fileFullPath, blobDataTime);
                                }
                                
                            }

                        }
                        else
                        {
                            pollFile(file, fileFullPath, blobDataTime);
                        }


                        
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        private void pollFile(CloudBlockBlob file, string fileFullPath, DateTime timestamp)
        {
            string directoryPath = Path.GetDirectoryName(fileFullPath);
            // Determine whether the directory exists.
            if (!Directory.Exists(directoryPath))
            {
                // create the directory.
                DirectoryInfo di = Directory.CreateDirectory(directoryPath);
            }

            file.DownloadToFile(fileFullPath, FileMode.Create);
            File.SetLastWriteTime(fileFullPath, TimeZoneInfo.ConvertTimeFromUtc(timestamp, TimeZoneInfo.Local));

        }


        private string localSyncFolderPath()
        {
            LocalDB readLocalDB = new LocalDB();
            readLocalDB=readLocalDB.readfromfile();

            return readLocalDB.getPath();
        }

        private void scanAllFiles()
        {
            string[] filePaths = Directory.GetFiles(clientSynFolderPath,"*",
                                         SearchOption.AllDirectories);

            foreach(string file in filePaths){
                CloudBlobContainer container = new CloudBlobContainer(new Uri(sasUri));
                CloudBlockBlob blob = container.GetBlockBlobReference(getPathInsync(file));
                if (!blob.Exists())
                {
                    try
                    {
                        System.IO.File.Delete(file);
                        Console.WriteLine("Delete file:" +file);
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
               
            }
        }

        private string getPathInsync(string fullpathOfChnagedFile)
        {
            string[] pathName = clientSynFolderPath.Split('\\');
            string[] pathName2 = fullpathOfChnagedFile.Split('\\');
            int i = pathName2.Count();
            string pathInSyncFolderPath = "";
            for (int j = pathName.Count(); j < i; j++)
            {
                pathInSyncFolderPath += pathName2[j];
                if ((j + 1) < i)
                {
                    pathInSyncFolderPath += "\\";
                }
            }

            return pathInSyncFolderPath;
        }

    }
}
