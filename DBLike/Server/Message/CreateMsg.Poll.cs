﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.MessageClasses;

namespace Server.Message
{
    public partial class CreateMsg
    {
        /// <summary>
        /// poll response protocol
        /// +-------------------------------------------+
        /// |OK::<POLL>:<fileContainerUri>:<fileBlobUri>|
        /// +-------------------------------------------+
        /// </summary>
        /// <param name="msgPoll"></param>
        /// <returns></returns>
        public string pollRespMsg(MsgPoll msgPoll)
        {
            string msg = "<OK>:<POLL>:<" + 
                         msgPoll.fileContainerUri  + ">:<" +
                         msgPoll.fileBlobUri + ">";
            return msg;
        }
    }
}
