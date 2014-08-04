using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Client.LocalFileSysAccess
{
    public class getFileAttributes
    {
        public DateTime lastModified { get; set; }
        public string md5Value { get; set; }
        public bool isDirectory { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public getFileAttributes(string filePath)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(filePath);

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isDirectory = true;
                    lastModified = System.IO.File.GetLastWriteTime(filePath);
                    md5Value = "isDirectory";
                    //System.IO.File.WriteAllText("datttt.txt", lastModified.ToString("MM/dd/yyyy HH:mm:ss"));
                }
                else
                {
                    isDirectory = false;
                    getTimeStamp(filePath);
                    getFileMD5Value(filePath);

                }
            }
            catch (Exception e)
            {
                Program.ClientForm.addtoConsole("Client.LocalFileSysAccess.getFileAttributes\n" + e.Message);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void getTimeStamp(string filePath)
        {

            // to UTC time
            // otherwise it's local time
            lastModified = System.IO.File.GetLastWriteTime(filePath).ToUniversalTime();

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void getFileMD5Value(string filePath)
        {

            MD5 md5Hasher = MD5.Create();
            StringBuilder sb = new StringBuilder();

            try
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    foreach (Byte b in md5Hasher.ComputeHash(fs))
                        sb.Append(b.ToString("x2").ToLower());
                    fs.Close();
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
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }
    }
}
