﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class CreateMsg
    {
        // take an ojbect, return a string
        public string createSignUpMsg(MessageClasses.MsgSignUp obj)
        {
            string str = "";

            /// sign up protocol
            /// +------------------------------------+
            /// |<signUp>:<userName>:<password>:<EOF>|
            /// +------------------------------------+
            str = "<SIGNUP>:<" + obj.userName + ">:<" + obj.psw + ">:<"+obj.nodeInfo+">:<EOF>";
            return str;
        }

    }
}
