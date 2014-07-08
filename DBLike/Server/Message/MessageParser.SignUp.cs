using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message
{
    public partial class MessageParser
    {
        // receive string from client, parse it to an obj

        /// sign up protocol
        /// +------------------------------------+
        /// |<signUp>:<userName>:<password>:<EOF>|
        /// +------------------------------------+
        /// 

        public Server.MessageClasses.MsgRespSignUp signUpParseMsg(string msg)
        {
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Server.MessageClasses.MsgRespSignUp obj = new Server.MessageClasses.MsgRespSignUp();
            obj.type = words[0];
            obj.userName = words[1];
            obj.psw = words[2];

            return obj;

        }



    }
}
