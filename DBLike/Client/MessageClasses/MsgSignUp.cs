using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgSignUp
    {
<<<<<<< HEAD
        String username;
        String password;
        String path;

        public MsgSignUp(String user, string pass)
        {
            this.username = user;
            this.password = pass;
        }

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
            return this.username;
        }

        public void setPassword(string Pass)
        {
            this.password = Pass;
=======
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
>>>>>>> origin/master
        }
    }
}
