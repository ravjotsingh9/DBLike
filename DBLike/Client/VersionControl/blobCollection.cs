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
    class blobCollection
    {
        public List<CloudBlockBlob> blobList { get; set; }
        public List<string> blobNames { get; set; }

        public List<CloudBlockBlob> snapShotList { get; set; }
        public List<string> snapShotNames { get; set; }
    }
}
