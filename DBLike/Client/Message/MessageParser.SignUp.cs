using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class MessageParser
    {

        public Client.MessageClasses.MsgSignUp signUpParseMessage(string msg)
        {
            // take a string(received from server), return an object
            /// sign up response protocol
            /// +----------------------------------+
            /// |<OK/ERRORS>:<additional Msg>:<EOF>|
            /// +----------------------------------+
            /// 

            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Client.MessageClasses.MsgSignUp msgSignUp = new Client.MessageClasses.MsgSignUp();


            msgSignUp.ack = words[0];
            msgSignUp.addiMsg = words[1];
            
            return msgSignUp;


        }

    }
}
