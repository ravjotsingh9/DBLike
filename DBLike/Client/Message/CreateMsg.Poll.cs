using System;
using Client.MessageClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class CreateMsg
    {
        /// <summary>
        /// poll protocol
        /// +--------------------------+
        /// |POLL:<userName>:<password>|
        /// +--------------------------+
        /// </summary>
        /// <param name="msgpoll"></param>
        /// <returns></returns>
        public string pollMsg(MsgPoll msgpoll)
        {
            string msg = "<UPLOAD>:<" + msgpoll.userName + ">:<" + msgpoll.password + ">";

            return msg;
        }
    }
}
