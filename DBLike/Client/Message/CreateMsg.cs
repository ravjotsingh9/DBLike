using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class CreateMsg
    {
        public string signInMsg(MessageClasses.MsgSignIn obj)
        {
            String user = obj.getUsername();
            String pass = obj.getPassword();

            /// sign up protocol
            /// +------------------------------------+
            /// |<signIp>:<userName>:<password>:<EOF>|
            /// +------------------------------------+
            string msg = "<signin>:<" + user + ">:<" + pass + ">:<";
            msg += "<EOF>";
            return msg;
        }

        public string signUpMsg(MessageClasses.MsgSignUp obj)
        {
            String user = obj.getUsername();
            String pass = obj.getPassword();

            /// sign up protocol
            /// +------------------------------------+
            /// |<signUp>:<userName>:<password>:<EOF>|
            /// +------------------------------------+
            string msg = "<signup>:<" + user + ">:<" + pass + ">:<";
            msg += "<EOF>";
            return msg;
        }
    }
}
