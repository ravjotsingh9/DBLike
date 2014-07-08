using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class MessageParser
    {

        public Client.MessageClasses.MsgSignUp.resp signUpRespParser(string msg)
        {
            // take a string(received from server), return an object
            /// sign up response protocol
            /// +-----------------+
            /// |<OK/ERRORS>:<EOF>|
            /// +-----------------+
            /// 

            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Client.MessageClasses.MsgSignUp.resp msgSignUp = new Client.MessageClasses.MsgSignUp.resp();


            msgSignUp.ack = words[0];
            return msgSignUp;


        }

    }
}
