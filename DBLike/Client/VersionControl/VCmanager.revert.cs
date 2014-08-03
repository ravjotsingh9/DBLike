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
        public void revertFromSnapshot(CloudBlobContainer container, CloudBlockBlob blobRef, CloudBlockBlob snapshot)
        {

            try
            {

                string blobPrefix = null;
                bool useFlatBlobListing = true;

                var snapshots = container.ListBlobs(blobPrefix, useFlatBlobListing,
                BlobListingDetails.Snapshots).Where(item => ((CloudBlockBlob)item).SnapshotTime.HasValue && item.Uri.Equals(blobRef.Uri)).ToList<IListBlobItem>();

                if (snapshots.Count < 1)
                {
                    Console.WriteLine("snapshot was not created");
                }
                else
                {

                    blobRef.StartCopyFromBlob(snapshot);
                    System.Windows.Forms.MessageBox.Show("revert success: " + snapshot.Name + " " + snapshot.SnapshotTime);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }


        public blobCollection listSnapshot(CloudBlobContainer container, CloudBlockBlob blobRef)
        {
            blobCollection blobset = new blobCollection();
            List<CloudBlockBlob> tempSnapshots = new List<CloudBlockBlob>();
            List<string> tempSnamshotsName = new List<string>();

            try
            {
                string blobPrefix = null;
                bool useFlatBlobListing = true;

                var snapshots = container.ListBlobs(blobPrefix, useFlatBlobListing,
                BlobListingDetails.Snapshots).Where(item => ((CloudBlockBlob)item).SnapshotTime.HasValue && item.Uri.Equals(blobRef.Uri)).ToList<IListBlobItem>();

                if (snapshots.Count < 1)
                {
                    Console.WriteLine("snapshot was not created");
                }
                else
                {
                    foreach (IListBlobItem item in snapshots)
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        blob.FetchAttributes();
                        Console.WriteLine("Name: {0}, Timestamp: {1}", blob.Name, blob.Metadata["timestamp"]);
                        tempSnapshots.Add(blob);
                        tempSnamshotsName.Add(blob.Name);
                        blobset.snapShotList = tempSnapshots;
                        blobset.snapShotNames = tempSnamshotsName;
                    }
                    //snapshots.ForEach(item => Console.WriteLine(String.Format("{0} {1} ", ((CloudBlockBlob)item).Name, ((CloudBlockBlob)item).Metadata["timestamp"])));
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            return blobset;
        }
    }
}
