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
using Client.Threads;
using Client.VersionControl;

namespace Client.PollFunction
{
    public class Poll
    {
        private string clientSynFolderPath;
        private string sasUri;

        public Poll(string sasUri)
        {
            //MessageBox.Show("Calling Poll constructor", "Client");
            Program.ClientForm.addtoConsole("Poll Constructor Called");
            this.clientSynFolderPath = localSyncFolderPath();
            
            this.sasUri = sasUri;
            
            scanAllFiles();
            Program.ClientForm.addtoConsole("Scanned all files");
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
                        Program.ClientForm.addtoConsole("file.MetaData: " + (file.Metadata["timestamp"]).ToString());
                        //Program.ClientForm.addtoConsole("using parser:" + DateTime.Parse((file.Metadata["timestamp"]).ToString()));
                        //blobDataTime = DateTime.ParseExact(file.Metadata["timestamp"], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        blobDataTime = DateTime.Parse(file.Metadata["timestamp"]);
                        if(File.Exists(fileFullPath))
                        {
                            getFileAttributes fileAttributes = new getFileAttributes(fileFullPath);
                            /*
                            DateTime blobDataTime = new DateTime();
                            blobDataTime = DateTime.ParseExact(file.Metadata["timestamp"], "MM-dd-yyyy HH:mm:ss",
                                                                null);
                             */ 
                            // if need to poll
                            Program.ClientForm.addtoConsole("Comparing Datetime for "+fileFullPath);
                            Program.ClientForm.addtoConsole("blobDateTime: "+blobDataTime);
                            Program.ClientForm.addtoConsole("lastModified: "+ fileAttributes.lastModified.ToUniversalTime() );
                            if (DateTime.Compare(blobDataTime, fileAttributes.lastModified.ToUniversalTime()) > 0)
                            {
                                Program.ClientForm.addtoConsole("Comparing HashValue");
                                Program.ClientForm.addtoConsole("Remote: " + file.Metadata["hashValue"]);
                                Program.ClientForm.addtoConsole("Local: "+ fileAttributes.md5Value);
                                if (fileAttributes.md5Value != file.Metadata["hashValue"])
                                {
                                    //it means there some change at server
                                    Program.ClientForm.addtoConsole("Poll function called");
                                    pollFile(file, fileFullPath, blobDataTime);
                                }
                                Program.ClientForm.addtoConsole("Hash Value Matched.No need to Download");
                            }
                            else if(DateTime.Compare(blobDataTime, fileAttributes.lastModified.ToUniversalTime()) < 0)
                            {
                                Program.ClientForm.addtoConsole("Comparing HashValue");
                                Program.ClientForm.addtoConsole("Remote: " + file.Metadata["hashValue"]);
                                Program.ClientForm.addtoConsole("Local: " + fileAttributes.md5Value);
                                if (fileAttributes.md5Value != file.Metadata["hashValue"])
                                {
                                    //it means there is some change at client which has yet not uploaded
                                    string eventType = "change";
                                    //LocalFileSysAccess.getFileAttributes timestamp = new LocalFileSysAccess.getFileAttributes(file);
                                    fileBeingUsed.eventDetails eventdet = new fileBeingUsed.eventDetails();
                                    eventdet.datetime = fileAttributes.lastModified;
                                    eventdet.filepath = fileFullPath;
                                    eventdet.eventType = eventType;
                                    if (Client.Program.filesInUse.alreadyPresent(eventdet))
                                    {
                                        //return;
                                        Program.ClientForm.addtoConsole("Already Present in the event list");
                                    }
                                    else
                                    {
                                        Client.Program.filesInUse.addToList(eventdet);
                                        Uploader upload = new Uploader();
                                        upload.start(fileFullPath, "change", null,fileAttributes.lastModified);
                                        Program.ClientForm.addtoConsole("Upload Thread Started for:"+ fileFullPath);
                                    }
                                    
                                }
                                Program.ClientForm.addtoConsole("Hash Value Matched.No need to upload");
                            }
                            Program.ClientForm.addtoConsole("Timestamp Matched.");
                        }
                        else
                        {
                            Program.ClientForm.addtoConsole("File Doesnot Exist");
                            Program.ClientForm.addtoConsole("Poll function Called");
                            pollFile(file, fileFullPath, blobDataTime);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Program.ClientForm.addtoConsole("Poll Exception:" + e.Message);
                Console.WriteLine(e.Message);
                MessageBox.Show(e.Message);
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

            /*****comment out these lines if do not want to download from snapshot
            VCmanager vc = new VCmanager(this.sasUri);
            LocalFileSysAccess.LocalFileSys save = new LocalFileSys();
            save.downloadfile(vc.getLatestSnapshot(file), fileFullPath, timestamp);
            *****comment out these lines if do not want to download from snapshot**/
            
            //****download from original file************************
            LocalFileSysAccess.LocalFileSys save = new LocalFileSys();
            save.downloadfile(file, fileFullPath, timestamp);
            //******************************************************//
            
            //updatetimestamp.settimestamp(fileFullPath, timestamp);
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
                        if (DialogResult.No == MessageBox.Show("DBLike is going to delete " + file + ". Do you want to keep it?", "DBLike Client", MessageBoxButtons.YesNo))
                        {
                            System.IO.File.Delete(file);
                            Console.WriteLine("Delete file:" + file);
                        }
                        else
                        {
                            string eventType = "create";
                            LocalFileSysAccess.getFileAttributes timestamp = new LocalFileSysAccess.getFileAttributes(file);
                            fileBeingUsed.eventDetails eventdet = new fileBeingUsed.eventDetails();
                            eventdet.datetime = timestamp.lastModified;
                            eventdet.filepath = file;
                            eventdet.eventType = eventType;
                            if (Client.Program.filesInUse.alreadyPresent(eventdet))
                            {
                                //return;
                            }
                            else
                            {
                                Client.Program.filesInUse.addToList(eventdet);
                                Uploader upload = new Uploader();
                                upload.start(file, "create", null, timestamp.lastModified);
                            }
                            
                        } 
                    }
                    catch (System.IO.IOException e)
                    {
                        Program.ClientForm.addtoConsole("Scanning Exception:"+ e.Message);
                        Console.WriteLine(e.Message);
                        MessageBox.Show(e.Message);
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
