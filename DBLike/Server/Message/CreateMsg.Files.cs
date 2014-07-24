using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message
{
    public partial class CreateMsg
    {
        /// <summary>
        /// upload response protocol
        /// +------------------------------------------------------------------+
        /// |OK:<upload>:<File path>:<File Container Uri>:<File Blob Uri>:<EOF>|
        /// +------------------------------------------------------------------+
        /// </summary>
        /// <param name="filePathInSynFolder"></param>
        /// <param name="filecontainerUri"></param>
        /// <param name="fileBlobUri"></param>
        /// <returns></returns>
        public string uploadRespMsg(string filePathInSynFolder, string filecontainerUri, string fileBlobUri, string addiInfo)
        {
            string msg = "<OK>:<upload>:<" +
                          filePathInSynFolder + ">:<" +
                          filecontainerUri + ">:<" +
                          fileBlobUri + ">:<" + addiInfo + ">:<EOF>";
            return msg;
        }
    }
}
