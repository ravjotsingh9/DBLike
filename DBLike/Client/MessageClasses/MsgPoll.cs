using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgPoll
    {
        // Client to Server
        public string userName { get; set; }
        public string password { get; set; }
        

        //Server to Client
        public string fileContainerUri { get; set; }
        public string fileBlobUri { get; set; }
        // for both
        public string indicator { get; set; }
    }
}
