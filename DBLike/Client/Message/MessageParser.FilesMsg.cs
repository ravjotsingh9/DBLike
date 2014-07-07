using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class MessageParser
    {
        public string filePathInSynFolder { get; set; }
        public string fileContainerUri { get; set; }
        public string fileBlobUri { get; set; }
        
        /// <summary>
        /// parse protocol
        /// +--------------------------------------------------------------------+
        /// |OK|:|upload|:|File path|:|File Container Uri|:|File Blob Uri|:|<EOF>|
        /// +--------------------------------------------------------------------+
        /// </summary>
        /// <param name="words"></param>
        public void uploadParseMsg(string[] words)
        {
            
            filePathInSynFolder = words[2];
            fileContainerUri = words[3];
            fileBlobUri = words[4];

        }
    }
}
