using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace Client.UploadFunctions
{

    public class UploadFile
    {
        /// <summary>
        /// Upload files by using Container Uri
        /// </summary>
        /// <param name="sasUri"></param>
        /// <param name="localFilePath"></param>
        /// <param name="filePathInSyncFolder"></param>
        public bool UploadFileWithContainerUri(string sasUri, string localFilePath,
            string filePathInSyncFolder, string fileHashVaule, DateTime fileTimestamp)
        {
            try
            {
                CloudBlobContainer container = new CloudBlobContainer(new Uri(sasUri));

                if (fileHashVaule == "isDirectory")
                {
                    CloudBlockBlob blob = container.GetBlockBlobReference(filePathInSyncFolder);
                    LocalFileSysAccess.LocalFileSys uploadfromFile = new LocalFileSysAccess.LocalFileSys();
                    uploadfromFile.uploadfromFilesystem(blob, localFilePath);
                    //blob.UploadFromFile(localFilePath, FileMode.Open);
                    //directory.Metadata["hashValue"] = fileHashVaule;


                }
                else
                {
                    CloudBlockBlob blob = container.GetBlockBlobReference(filePathInSyncFolder);
                    LocalFileSysAccess.LocalFileSys uploadfromFile = new LocalFileSysAccess.LocalFileSys();
                    uploadfromFile.uploadfromFilesystem(blob, localFilePath);
                    //blob.UploadFromFile(localFilePath, FileMode.Open);
                    blob.Metadata["hashValue"] = fileHashVaule;
                    blob.Metadata["timestamp"] = fileTimestamp.ToUniversalTime().ToString("MM/dd/yyyy HH:mm:ss");
                    blob.Metadata["filePath"] = filePathInSyncFolder;
                    blob.SetMetadata();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }



        



    }
}
