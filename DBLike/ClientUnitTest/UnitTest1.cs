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
    }
}
