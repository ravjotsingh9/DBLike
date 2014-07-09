using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MessageClasses
{
    public class MsgRespUpload
    {
        /**
        public string type { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string filePathInSynFolder { get; set; }
        public DateTime fileTimeStamps { get; set; }
        public string fileHashValue { get; set; }
        public string fileName { get; set; }
         
         * */


        public string indicator { get; set; }
        public string filePathInSynFolder { get; set; }
        public string fileContainerUri { get; set; }
        public string fileBlobUri { get; set; }
        public string type { get; set; }
    }
}
