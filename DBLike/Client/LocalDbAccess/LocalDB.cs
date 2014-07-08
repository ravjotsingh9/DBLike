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
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + "//dblike.txt", false);
            file.Write(pathofFoldertoSync);
            return true;
        }
        public bool readfromfile()
        {/*
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            System.IO.StreamReader file = new System.IO.StreamReader("c:\\test.txt");
          */
            return true;
        }
    }
}
