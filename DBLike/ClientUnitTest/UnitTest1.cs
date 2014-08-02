using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.BlobAccess;
using Client.MessageClasses;
using Server.MessageClasses;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Server.ConnectionManager;
using Client;
using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Client.Threads;
using Client.LocalDbAccess;
using Client.LocalFileSysAccess;

namespace ClientUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void dicTest()
        {
            Client.LocalFileSysAccess.FileInfo tmp = new Client.LocalFileSysAccess.FileInfo();
            tmp.time = DateTime.UtcNow;
            tmp.md5r = "hashvalue";


            Client.LocalFileSysAccess.FileInfo tmp2 = new Client.LocalFileSysAccess.FileInfo();
            tmp2.time = DateTime.UtcNow;
            tmp2.md5r = "hashvalue";

            // add for regular dictionary
            //Client.LocalFileSysAccess.FileList.fileInfoDic.Add("c:\\", tmp);
            //Client.LocalFileSysAccess.FileList.fileInfoDic.Add("c:\\test", tmp2);

            // add for ConcurrentDictionary
            Client.LocalFileSysAccess.FileList.fileInfoDic["c:\\"] = tmp;
            Client.LocalFileSysAccess.FileList.fileInfoDic["c:\\test"] = tmp2;


            string str = "";
        }



        //[TestMethod]
        // before scanAllFilesAttributes() changing to void
        //public void addToDicTest()
        //{
        //    Client.LocalFileSysAccess.FileListMaintain maintain = new Client.LocalFileSysAccess.FileListMaintain();
        //    string[] filePaths = maintain.scanAllFilesAttributes();

        //    foreach (string file in filePaths)
        //    {
        //        try
        //        {
        //            Client.LocalFileSysAccess.FileInfo tmp = null;

        //            // get file attributes
        //            Client.LocalFileSysAccess.getFileAttributes getFileAttr = new Client.LocalFileSysAccess.getFileAttributes(file);

        //            tmp = new Client.LocalFileSysAccess.FileInfo();
        //            tmp.time = getFileAttr.lastModified;
        //            tmp.md5r = getFileAttr.md5Value;

        //            Client.LocalFileSysAccess.FileList.fileInfoDic[file] = tmp;

        //        }
        //        catch (System.IO.IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return;
        //        }
        //    }
        //    string str = "";
        //}
    }
}
