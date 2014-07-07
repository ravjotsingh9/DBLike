using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ConnectionManager
{
    public partial class GenerateSAS
    {
        
        /// <summary>
        /// Create share accesss policy for READ, WRITE, LIST and DELETE
        /// </summary>
        /// <param name="blobClient"></param>
        /// <param name="container"></param>
        /// <param name="policyName"></param>
        private void CreateSASRWLD(CloudBlobClient blobClient, CloudBlobContainer container, string policyName)
        {
            policyName = "RWLD";
            
            //Create a new stored access policy and define its constraints.
            SharedAccessBlobPolicy sharedPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List|SharedAccessBlobPermissions.Delete
            };

            //Get the container's existing permissions.
            BlobContainerPermissions permissions = new BlobContainerPermissions();

            //Add the new policy to the container's permissions.
            permissions.SharedAccessPolicies.Clear();
            permissions.SharedAccessPolicies.Add(policyName, sharedPolicy);
            container.SetPermissions(permissions);
        }
    }
}
