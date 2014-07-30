using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message
{
    public partial class CreateMsg
    {
        // take an obj ( already filled with data)
        // then return a string to send to the client
        /// sign in response protocol
        /// +----------------------------------+
        /// |<OK/ERRORS>:<additional Msg>:<EOF>|
        /// +----------------------------------+
        /// 

        public string signInResp(Server.MessageClasses.MsgSignIn.resp obj)
        {

            // MINOR CHANGE, TO BE TESTED
            string str = "<" + obj.ack + ">:<" + obj.addiMsg + ">:<EOF>";
            return str;
        }

    }
}
