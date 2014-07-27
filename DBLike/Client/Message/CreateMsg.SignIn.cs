using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class CreateMsg
    {
        // take an ojbect, return a string
        public string createSignInMsg(MessageClasses.MsgSignIn obj)
        {
           string str = "";

           /// sign in protocol
           /// +------------------------------------+
           /// |<SignIn>:<userName>:<password>:<EOF>|
           /// +------------------------------------+
        str = "<SIGNIN>:<" + obj.getUsername() + ">:<" + obj.getPassword() + ">:<EOF>";
       return str;
       }
    }
}
