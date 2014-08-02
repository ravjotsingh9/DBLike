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
            file.DownloadToFile(fileFullPath, FileMode.Create);
            //file.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
            File.SetLastWriteTime(fileFullPath, TimeZoneInfo.ConvertTimeFromUtc(timestamp, TimeZoneInfo.Local));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void uploadfromFilesystem(CloudBlockBlob blob, string localFilePath, string eventType)
        {
            if (eventType.Equals("create"))
            {
                blob.UploadFromFile(localFilePath, FileMode.Open);
            }
            else
            {
                try
                {
                    string leaseId = Guid.NewGuid().ToString();
                    blob.AcquireLease(TimeSpan.FromMilliseconds(16000), leaseId);
                    blob.UploadFromFile(localFilePath, FileMode.Open, AccessCondition.GenerateLeaseCondition(leaseId));
                    blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
                }
                catch(Exception ex)
                {
                    Thread.Sleep(5000);
                    string leaseId = Guid.NewGuid().ToString();
                    blob.AcquireLease(TimeSpan.FromMilliseconds(16000), leaseId);
                    blob.UploadFromFile(localFilePath, FileMode.Open, AccessCondition.GenerateLeaseCondition(leaseId));
                    blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
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
