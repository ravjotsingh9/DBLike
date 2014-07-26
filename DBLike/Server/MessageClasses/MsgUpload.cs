using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MessageClasses
{
    public class MsgUpload
    {

        public string type { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string filePathInSynFolder { get; set; }
        public DateTime fileTimeStamps { get; set; }
        public string fileHashValue { get; set; }
        public string fileName { get; set; }

        // for upload, deletion, or renaming
        public string addInfo { get; set; }

    }
}
