using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client.LocalFileSysAccess
{
    public class getFileAttributes
    {
        public DateTime getTimeStamp(string filePath){
            
            DateTime lastModified = System.IO.File.GetLastWriteTime(filePath);
            return lastModified;
        }

        public string getFileMD5Value(string filePath)
        {
           
            MD5 md5Hasher = MD5.Create();
            StringBuilder sb = new StringBuilder();

            using (FileStream fs = File.OpenRead(filePath))
            {
                foreach (Byte b in md5Hasher.ComputeHash(fs))
                    sb.Append(b.ToString("x2").ToLower());
            }

            string hexString = sb.ToString();
            
            byte[] buffer = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i++)
            {
                buffer[i / 2] = Convert.ToByte(Convert.ToInt32(hexString.Substring(i, 2), 16));
                i += 1;
            }
            string res = Convert.ToBase64String(buffer);
                
            
            return res;
        }
    }
}
