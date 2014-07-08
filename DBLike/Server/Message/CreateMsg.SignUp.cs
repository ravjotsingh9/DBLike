using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message
{
    // to send to the client
    public partial class CreateMsg
    {
        // take an obj ( already filled with data)
        // then return a string to send to the client
        /// sign up response protocol
        /// +----------------------------------+
        /// |<OK/ERRORS>:<additional Msg>:<EOF>|
        /// +----------------------------------+
        /// 

        public string signUpResp(Server.MessageClasses.MsgSignUp.resp obj)
        {

            // MINOR CHANGE, TO BE TESTED
            string str = "<" + obj.ack + ">:<" + obj.addiMsg + ">:<EOF>";
            return str;
        }

    }
}
