using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Client.LocalDbAccess;
using Client.LocalFileSysAccess;
using System.Windows.Forms;
using Client.Threads;

namespace Client.LocalFileSysAccess
{
    public class FileListMaintain
    {

        // update single file to fileList
        public void updateSingleFileToFileList(string fullFilePath, DateTime dt, string hashValue)
        {
            try
            {
                Client.LocalFileSysAccess.FileList.fileInfoDic[fullFilePath].md5r = hashValue;
                Client.LocalFileSysAccess.FileList.fileInfoDic[fullFilePath].time = dt;
            }
            catch (System.IO.IOException e)
            {
                System.Windows.Forms.MessageBox.Show("FileListMaintain <-" + e.Message);
            }
        }



        // add single file to file list
        public void addSingleFileToFileList(string filePath)
        {

            Client.LocalFileSysAccess.FileInfo tmp = null;

            // get file attributes
            Client.LocalFileSysAccess.getFileAttributes getFileAttr = new Client.LocalFileSysAccess.getFileAttributes(filePath);

            tmp = new Client.LocalFileSysAccess.FileInfo();
            tmp.time = getFileAttr.lastModified;
            tmp.md5r = getFileAttr.md5Value;

            // add to fileList
            Client.LocalFileSysAccess.FileList.fileInfoDic[filePath] = tmp;

        }

        public void scanAllFilesAttributes()
        {
            // get local path
            LocalDB readLocalDB = new LocalDB();
            readLocalDB = readLocalDB.readfromfile();

            string clientSynFolderPath = readLocalDB.getPath();





            string[] filePaths = Directory.GetFiles(clientSynFolderPath, "*", SearchOption.AllDirectories);

            //return filePaths;
            //foreach (string file in filePaths)
            //{
            //    try
            //    {
            //        // get file attributes
            //        Client.LocalFileSysAccess.getFileAttributes getFileAttr = new Client.LocalFileSysAccess.getFileAttributes(file);

            //        string md5 = getFileAttr.md5Value;
            //        DateTime dt = getFileAttr.lastModified;




            //    }
            //    catch (System.IO.IOException e)
            //    {
            //        Console.WriteLine(e.Message);
            //        MessageBox.Show(e.Message);
            //        return;
            //    }


            foreach (string file in filePaths)
            {
                try
                {
                    // new a FileInfo instance to hold each file's metadata
                    Client.LocalFileSysAccess.FileInfo tmp = null;

                    // get file attributes
                    Client.LocalFileSysAccess.getFileAttributes getFileAttr = new Client.LocalFileSysAccess.getFileAttributes(file);

                    tmp = new Client.LocalFileSysAccess.FileInfo();
                    tmp.time = getFileAttr.lastModified;
                    tmp.md5r = getFileAttr.md5Value;

                    // add to fileList
                    Client.LocalFileSysAccess.FileList.fileInfoDic[file] = tmp;

                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

            }
        }
    }
}
