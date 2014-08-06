using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.LocalFileSysAccess
{
    class LocalFileSys
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void downloadfile(CloudBlockBlob file, string fileFullPath, DateTime timestamp)
        {
            //string leaseId = Guid.NewGuid().ToString();
            //file.AcquireLease(TimeSpan.FromMilliseconds(16000), leaseId);
            Program.ClientForm.addtoConsole("Download started:" + fileFullPath);
            file.DownloadToFile(fileFullPath, FileMode.Create);
            Program.ClientForm.addtoConsole("Downloaded! File : " + fileFullPath);
            Program.ClientForm.ballon("Downloaded:" + fileFullPath);
            //file.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
            File.SetLastWriteTime(fileFullPath, TimeZoneInfo.ConvertTimeFromUtc(timestamp, TimeZoneInfo.Local));



            // add file to file list
            Client.LocalFileSysAccess.FileListMaintain addToFileList = new Client.LocalFileSysAccess.FileListMaintain();
            addToFileList.addSingleFileToFileList(fileFullPath);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void uploadfromFilesystem(CloudBlockBlob blob, string localFilePath, string eventType)
        {

            if (eventType.Equals("create") || eventType.Equals("signUpStart"))
            {
                Program.ClientForm.addtoConsole("Upload started[create || signUpStart]:" + localFilePath);
                blob.UploadFromFile(localFilePath, FileMode.Open);
                Program.ClientForm.addtoConsole("Uploaded");
                Program.ClientForm.ballon("Uploaded:"+ localFilePath);
            }
            else
            {
                try
                {
                    Program.ClientForm.addtoConsole("Upload started[change,etc]:" + localFilePath);
                    string leaseId = Guid.NewGuid().ToString();
                    blob.AcquireLease(TimeSpan.FromMilliseconds(16000), leaseId);
                    blob.UploadFromFile(localFilePath, FileMode.Open, AccessCondition.GenerateLeaseCondition(leaseId));
                    blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
                    Program.ClientForm.addtoConsole("Uploaded");
                    Program.ClientForm.ballon("Uploaded:" + localFilePath);
                }
                catch (Exception ex)
                {
                    Program.ClientForm.addtoConsole("Upload: second attempt");
                    Thread.Sleep(5000);
                    string leaseId = Guid.NewGuid().ToString();
                    blob.AcquireLease(TimeSpan.FromMilliseconds(16000), leaseId);
                    blob.UploadFromFile(localFilePath, FileMode.Open, AccessCondition.GenerateLeaseCondition(leaseId));
                    blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
                    Program.ClientForm.addtoConsole("Uploaded");
                    Program.ClientForm.ballon("Uploaded:" + localFilePath);
                }
            }
        }

        // return true if it's a dir
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool checkIfDirectory(string path)
        {

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            else
                return false;

        }
    }
}
