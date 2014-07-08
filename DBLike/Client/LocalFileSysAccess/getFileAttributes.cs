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
        public DateTime lastModified { get; set; }
        public string md5Value { get; set; }

        public getFileAttributes(string filePath)
        {
            getTimeStamp(filePath);
            getFileMD5Value(filePath);
        }
        
        private void getTimeStamp(string filePath){
            
            lastModified = System.IO.File.GetLastWriteTime(filePath);
            
        }

        private void getFileMD5Value(string filePath)
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
            md5Value = Convert.ToBase64String(buffer);
                
        }
    }
}
