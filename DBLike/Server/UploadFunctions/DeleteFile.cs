using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using Microsoft.WindowsAzure.Storage.Blob;
using Server.BlobAccess;

namespace Server.UploadFunctions
{
    public class DeleteFile
    {

        // delete one item
        public void deleteItem(CloudBlobContainer container, string pathInSyncFolder)
        {

            // get blob
            var item = container.GetBlobReferenceFromServer(pathInSyncFolder);
            try
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blob.Delete();
                    System.Windows.Forms.MessageBox.Show(string.Format("File: {0} Deleted!", pathInSyncFolder), "DBLike Server");

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blob.Delete();
                    System.Windows.Forms.MessageBox.Show(string.Format("File: {0} Deleted!", pathInSyncFolder), "DBLike Server");
                }
            }
            catch
            {
                throw;
            }
        }


        // delete folders
        public void deleteFolder(CloudBlobContainer container, CloudBlobDirectory tempDir)
        {
            IEnumerable<IListBlobItem> blobs = tempDir.ListBlobs();

            foreach (IListBlobItem item in blobs)
            {

                // for items in subdirecories
                // parent directory doesn't need this
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blob.Delete();
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blob.Delete();
                }
                //else if (item.GetType() == typeof(CloudBlobDirectory))
                //{
                //    CloudBlobDirectory blob = (CloudBlobDirectory)item;
                //    blob.
                //}
                else
                {
                    CloudBlobDirectory blobDir = (CloudBlobDirectory)item;
                    deleteFolder(container, blobDir);
                }
            }


        }



    }
}
