using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.LocalFileSysAccess
{

    // for every file
    public class FileInfo
    {
        public DateTime time;
        public string md5r;
    }


    // local file list to store hashvalue and time stamp
    public static class FileList
    {
        public static string fullpathOfChnagedFile;

        // dic to store file timestamp and hashvalue
        public static Dictionary<string, FileInfo> fileInfoDic = new Dictionary<string, FileInfo>();


    }
}
