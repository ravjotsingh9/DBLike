using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgSignUp
    {

        public string userName { get; set; }
        public string psw { get; set; }

        // ack from server
        public string ack { get; set; }

        // additional msg from server
        public string addiMsg { get; set; }
    }
}
