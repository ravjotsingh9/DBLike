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
            try
            {
                IEnumerable<IListBlobItem> blobs = tempDir.ListBlobs();

                foreach (IListBlobItem item in blobs)
                {

                    // for items in subdirecories
                    // parent directory doesn't need this
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        //blob.Delete();
                        blob.DeleteIfExists();

                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob blob = (CloudPageBlob)item;
                        blob.DeleteIfExists();
                    }
                    //else if (item.GetType() == typeof(CloudBlobDirectory))
                    //{
                    //    CloudBlobDirectory blob = (CloudBlobDirectory)item;
                    //    blob.
                    //}
                    else
                    {
                        // delete folders recursively
                        CloudBlobDirectory blobDir = (CloudBlobDirectory)item;
                        deleteFolder(container, blobDir);
                    }
                }
            }
            catch
            {
                throw;
            }


        }






        public void deleteAll(CloudBlobContainer container, string pathInSyncFolder)
        {
            // update path on server
             pathInSyncFolder = pathInSyncFolder.Replace("\\", "/");
            
            // check if it is dir
            bool isDir = false;
            CloudBlobDirectory dir = container.GetDirectoryReference(pathInSyncFolder);
            List<IListBlobItem> blobs = dir.ListBlobs().ToList();

            if (blobs.Count != 0)
            {
                isDir = true;
            }

            try
            {

                if (!isDir)
                {
                    // this can only get the blob type, not the dir type
                    var item = container.GetBlobReferenceFromServer(pathInSyncFolder);

                    

                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        blob.DeleteIfExists();
                        //blob.Delete();
                        System.Windows.Forms.MessageBox.Show(string.Format("File: {0} Deleted!", pathInSyncFolder), "DBLike Server");

                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob blob = (CloudPageBlob)item;
                        //blob.Delete();
                        blob.DeleteIfExists();
                        //System.Windows.Forms.MessageBox.Show(string.Format("File: {0} Deleted!", pathInSyncFolder), "DBLike Server");
                    }
                }
                else
                {
                    // get the directory reference
                    CloudBlobDirectory dira = container.GetDirectoryReference(pathInSyncFolder);
                    deleteFolder(container, dira);
                    //System.Windows.Forms.MessageBox.Show(string.Format("Deleted!\n Folder: {0}", pathInSyncFolder), "DBLike Server");
                }

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show(string.Format("File: {0} \n doesn't exist!", pathInSyncFolder), "DBLike Server");
            }

        }

    }



}

