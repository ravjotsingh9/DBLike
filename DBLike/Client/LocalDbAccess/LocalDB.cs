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
            StreamWriter file = null;
            string path = @"C:\dblike.txt";
            if (!File.Exists(path))
            {
                file = new StreamWriter(path);
           }
           else
            {
                File.Delete(path);
                file = new StreamWriter(path);
            }
            file.WriteLine(username);
            file.WriteLine(Password);
            file.WriteLine(pathofFoldertoSync);
            file.Close();
            return true;
        }
        public string[] readfromfile()
        {
            LocalDbAccess.LocalDB fileattr = new LocalDbAccess.LocalDB();
            string path = @"C:\dblike.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string[] filedetails = new string[3];
                filedetails[0] = file.ReadLine();
                filedetails[1] = file.ReadLine();
                filedetails[2] = file.ReadLine();
                fileattr.setUsername(filedetails[0]);
                fileattr.setPassword(filedetails[1]);
                fileattr.setPath(filedetails[2]);
                return filedetails;
            }
            else
            {
                return null;
            }   
        }

        public bool isExists()
        {
            if (File.Exists(@"C:\dblike.txt"))
                return true;
            else
                return false;
        }
    }
}
