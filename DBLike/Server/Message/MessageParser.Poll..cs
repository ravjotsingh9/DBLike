using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.MessageClasses;

namespace Server.Message
{
    public partial class MessageParser
    {
        /// <summary>
        /// poll protocol
        /// +--------------------------+
        /// |POLL:<userName>:<password>|
        /// +--------------------------+
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public MsgPoll pollParseMsg(string msg)
        {
            MsgPoll msgPoll = new MsgPoll();
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            msgPoll.userName = words[1];
            msgPoll.password = words[2];
            msgPoll.nodeInfo = words[3];
            return msgPoll;
        }
        
    }
}
