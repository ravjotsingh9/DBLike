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
        /// Create share accesss permission
        /// </summary>
        /// <param name="blobClient"></param>
        /// <param name="container"></param>
        /// <param name="permissionType"></param>
        private SharedAccessBlobPolicy CreateSASPermission(string permissionType)
        {
            
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1);
            if (permissionType == "RWLD")
            {
                sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete;
            }
            else
            {
                sasConstraints.Permissions = SharedAccessBlobPermissions.None;
            }
            
            
            return sasConstraints;
        }
    }
}
