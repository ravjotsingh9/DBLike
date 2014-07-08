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
        public bool writetofile(string pathofFoldertoSync)
        {
            string path = Directory.GetCurrentDirectory();
            path += @"\dblike.txt";
            if (!File.Exists(path))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path);
                file.WriteLine(pathofFoldertoSync);
                return true;
            }
            else
            {
                return false;
            }
        }
        public string readfromfile()
        {
            String line = null;
            String path = Directory.GetCurrentDirectory();
            path += @"\dblike.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                line = file.ReadLine();
            }
            else
            {
                line = "File doesnot exists";
            }

            return line;
        }
    }
}
