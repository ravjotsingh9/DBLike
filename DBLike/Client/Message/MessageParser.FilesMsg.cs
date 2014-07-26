using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.MessageClasses;

namespace Client.Message
{
    public partial class MessageParser
    {
        
        /// <summary>
        /// parse protocol
        /// +------------------------------------------------------------------+
        /// |OK:<upload>:<File path>:<File Container Uri>:<File Blob Uri>:<addiInfo>:<EOF>|
        /// +------------------------------------------------------------------+
        /// </summary>
        /// <param name="words"></param>
        public MsgRespUpload uploadParseMsg(string msg)
        {
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            MsgRespUpload upload = new MsgRespUpload();
            upload.indicator = words[0];
            
            if (upload.indicator == "OK")
            {
                upload.type = words[1];
                upload.filePathInSynFolder = words[2];
                upload.fileContainerUri = words[3];
                upload.fileBlobUri = words[4];
                //upload.addiInfo = words[5];
                upload.addiInfo = words[5];
            }
            else 
            {
                upload.type = words[1];
            }
            

            return upload;
        }
    }
}
