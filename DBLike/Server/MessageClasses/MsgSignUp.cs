using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MessageClasses
{
    public class MsgSignUp
    {
        public class req
        {
            public string type { get; set; }
            public string userName { get; set; }
            public string psw { get; set; }
        }

        public class resp
        {
            // ack from server
            public string ack { get; set; }
        }
    }
}
