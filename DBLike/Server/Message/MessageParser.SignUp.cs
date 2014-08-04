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

        public Server.MessageClasses.MsgSignUp.req signUpParseReq(string msg)
        {
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Server.MessageClasses.MsgSignUp.req obj = new Server.MessageClasses.MsgSignUp.req();
            obj.type = words[0];
            obj.userName = words[1];
            obj.psw = words[2];
            obj.nodeInfo = words[3];
            return obj;

        }



    }
}
