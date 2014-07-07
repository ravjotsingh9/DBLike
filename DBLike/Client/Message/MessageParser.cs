using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class MessageParser
    {
        public MessageParser(string msg)
        {
            string[] separators = { ":<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (words[0] == "OK" && words[1] == "upload")
            {
                uploadParseMsg(words);
            }
        }
    }
}
