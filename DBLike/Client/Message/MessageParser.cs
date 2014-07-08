using Client.MessageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class MessageParser
    {
        public MsgSignUp signupParseMsg(string msg)
        {
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (words[0] == "OK")
            {
                MsgSignUp signup = new MsgSignUp(words[1], words[2]);
                return signup;
            }
            return null;
        }

        public MsgSignIn signinParseMsg(string msg)
        {
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (words[0] == "Ok")
            {
                MsgSignIn signin = new MsgSignIn(words[1], words[2]);
                return signin;
            }
            return null;
        }
    }
}
