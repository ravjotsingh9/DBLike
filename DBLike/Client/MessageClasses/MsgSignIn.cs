using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgSignIn
    {
        String username;
        String password;
        String ack;
        String addiMsg;

        public string getUsername()
        {
            return this.username;
        }

        public void setUsername(string user)
        {
            this.username = user;
        }

        public string getPassword()
        {
            return this.password;
        }

        public void setPassword(string Pass)
        {
            this.password = Pass;
        }

        public string getAck()
        {
            return this.ack;
        }

        public void setAck(string Ack)
        {
            this.ack = Ack;
        }

        public string getAddiMsg()
        {
            return this.addiMsg;
        }

        public void setAddiMsg(string addmsg)
        {
            this.addiMsg = addmsg;
        }

    }
}
