using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MessageClasses
{
    public class MsgRespSignUp
    {

        public string ack { get; set; }
        public string type { get; set; }
        public string userName { get; set; }
        public string psw { get; set; }

    }
}
