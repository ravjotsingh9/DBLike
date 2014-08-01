using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.LocalFileSysAccess
{
    class LocalFileSys
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void downloadfile(CloudBlockBlob file, string fileFullPath, DateTime timestamp)
        {
            file.DownloadToFile(fileFullPath, FileMode.Create);
            File.SetLastWriteTime(fileFullPath, TimeZoneInfo.ConvertTimeFromUtc(timestamp, TimeZoneInfo.Local));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void uploadfromFilesystem(CloudBlockBlob blob, string localFilePath)
        {
            blob.UploadFromFile(localFilePath, FileMode.Open);

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
