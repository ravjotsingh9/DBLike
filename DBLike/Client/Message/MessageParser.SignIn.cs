using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class MessageParser
    {
        public Client.MessageClasses.MsgSignIn signInParseMessage(string msg)
        {
             // take a string(received from server), return an object
            /// sign in response protocol
            /// +----------------------------------+
            /// |<OK/ERRORS>:<additional Msg>:<EOF>|
            /// +----------------------------------+
            
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Client.MessageClasses.MsgSignIn msgSignIn = new Client.MessageClasses.MsgSignIn();
            msgSignIn.setAck(words[0]);
            msgSignIn.setAddiMsg(words[1]);
            return msgSignIn;
        }
    }
}
