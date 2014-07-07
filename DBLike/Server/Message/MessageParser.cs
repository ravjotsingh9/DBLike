using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message
{
    public partial class MessageParser
    {
        public MessageParser(string msg)
        {
            string[] separators = { ":<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (words[0] == "upload")
            {
                uploadParseMsg(words);
            }
        }
    }
}
