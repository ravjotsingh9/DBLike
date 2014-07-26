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
        public MsgPoll pollParseMsg(string msg)
        {
            MsgPoll msgPoll = new MsgPoll();
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            msgPoll.indicator = words[0];

            if (msgPoll.indicator == "OK")
            {
                msgPoll.fileContainerUri = words[2];
                msgPoll.fileBlobUri = words[3];
            }

            return msgPoll;
        }
    }
}
