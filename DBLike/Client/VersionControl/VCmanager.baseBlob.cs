using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Client.VersionControl
{
    public partial class VCmanager
    {
        public blobCollection list()
        {

            blobCollection fileSet = new blobCollection();
            List<CloudBlockBlob> tempBlobList = new List<CloudBlockBlob>();
            List<string> tempBlobNames = new List<string>();
            try
            {
                string blobPrefix = null;
                bool useFlatBlobListing = true;
                var blobs = container.ListBlobs(blobPrefix, useFlatBlobListing, BlobListingDetails.None);
                foreach (IListBlobItem item in blobs)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob file = (CloudBlockBlob)item;

                        tempBlobList.Add(file);
                        tempBlobNames.Add(file.Name);
                        fileSet.blobList = tempBlobList;
                        fileSet.blobNames = tempBlobNames;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            return fileSet;
        }
    
    }
}
