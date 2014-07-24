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

namespace Client.PollFunction
{
    public class Poll
    {
        private string clientSynFolderPath;

        private bool ListWithContainerUri(string sasUri)
        {
            try
            {
                CloudBlobContainer container = new CloudBlobContainer(new Uri(sasUri));
                container.CreateIfNotExists();

                string blobPrefix = null;
                bool useFlatBlobListing = true;
                var blobs = container.ListBlobs(blobPrefix, useFlatBlobListing, BlobListingDetails.None);
                foreach (var item in blobs)
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
                        if(File.Exists(fileFullPath))
                        {
                            getFileAttributes fileAttributes = new getFileAttributes(fileFullPath);
                            DateTime blobDataTime = new DateTime();
                            blobDataTime = DateTime.ParseExact(file.Metadata["timestamp"], "MM-dd-yyyy HH:mm:ss",
                                                                null);
                            // if need to poll
                            if (DateTime.Compare(blobDataTime, fileAttributes.lastModified) > 0)
                            {
                                if (fileAttributes.md5Value != file.Metadata["hashValue"])
                                {
                                    pollFile(file, fileFullPath);
                                }
                                
                            }

                        }
                        else
                        {
                            pollFile(file, fileFullPath);
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

        public Poll(string sasUri)
        {
            this.clientSynFolderPath = localSyncFolderPath();
            ListWithContainerUri(sasUri);
        }

        private string localSyncFolderPath()
        {
            LocalDB readLocalDB = new LocalDB();
            readLocalDB.readfromfile();
            return readLocalDB.getPath();
        }

        private void pollFile(CloudBlockBlob file, string fileFullPath)
        {
            string directoryPath = Path.GetDirectoryName(fileFullPath);
            // Determine whether the directory exists.
            if (!Directory.Exists(directoryPath))
            {
                // create the directory.
                DirectoryInfo di = Directory.CreateDirectory(directoryPath);
            }

            file.DownloadToFile(fileFullPath, FileMode.Create);

        }

    }
}
