using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.LocalDbAccess
{
    class LocalDB
    {
        String Username;
        String password;
        String path;
        public string getUsername()
        {
            return this.Username;
        }

        public void setUsername(String user)
        {
            this.Username = user;
        }

        public string getPassword()
        {
            return this.password;
        }

        public void setPassword(String Pass)
        {
            this.password = Pass;
        }

        public string getPath()
        {
            return this.path;
        }

        public void setPath(String Path)
        {
            this.path = Path;
        }

        public bool writetofile(String username,String Password, string pathofFoldertoSync)
        {
            string path = Directory.GetCurrentDirectory();
            path += @"\dblike.txt";
            if (!File.Exists(path))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path);
                file.WriteLine(username);
                file.WriteLine(Password);
                file.WriteLine(pathofFoldertoSync);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool readfromfile()
        {
            String path = Directory.GetCurrentDirectory();
            path += @"\dblike.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                if (File.ReadLines(path).Count() == 3)
                {
                    String[] filedetails = new String[3];
                    filedetails[0] = file.ReadLine();
                    filedetails[1] = file.ReadLine();
                    filedetails[2] = file.ReadLine();
                    setUsername(filedetails[0]);
                    setPassword(filedetails[1]);
                    setPath(filedetails[2]);
                }
                return true;
            }
            else
            {
                return false;
            }   
        }
    }
}
