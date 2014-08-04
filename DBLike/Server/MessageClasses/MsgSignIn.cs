﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MessageClasses
{
    public class MsgSignIn
    {
        public class req
        {
            public string type { get; set; }
            public string userName { get; set; }
            public string psw { get; set; }
            public string nodeInfo { get; set; }
        }

        public class resp
        {
            // ack from server
            public string ack { get; set; }

            // additional msg to send to client
            public string addiMsg { get; set; }
        }
    }
}
