using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgSignUp
    {
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
        }

        public string getPath()
        {
            return this.path;
        }

        public void setPath(string Path)
        {
            this.path = Path;
        }
    }
}
