using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgUpload
    {
        public string indicator { get; set; }
        public string filePathInSynFolder { get; set; }
        public string fileContainerUri { get; set; }
        public string fileBlobUri { get; set; }
        public string type { get; set; }
    }
}
