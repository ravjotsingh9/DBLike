using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Threads
{
    class Uploader
    {
        static Configuration.config conf = new Configuration.config();
        static Thread thread;
        public void start(string path)
        {
            //TBD
            thread = new Thread(()=>threadStartFun(path));
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string fullpathOfChnagedFile)
        {
            //TBD
            //send msg to server
            //get the information from the localDatabase

            string clientSyncFolderPath="";
            string replacement = "";
            Regex rgx = new Regex(clientSyncFolderPath);
            string pathInSyncFolderPath = rgx.Replace(fullpathOfChnagedFile, replacement);
            string userName = "";
            string password = "";
            Client.Message.CreateMsg uploadM = new Client.Message.CreateMsg();
            Client.LocalFileSysAccess.getFileAttributes att = new Client.LocalFileSysAccess.getFileAttributes(fullpathOfChnagedFile);
            DateTime time = att.lastModified;
            string md5r = att.md5Value;
            string msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, time, md5r);

            //send the msg using socket
            ConnectionManager.Connection conn = new ConnectionManager.Connection();
            Socket soc = conn.connect(conf.serverAddr, conf.port);

            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            rw.writetoSocket(soc, msg);
            //receive the msg
            string resp = rw.readfromSocket(soc);
            
            //8 Client parse msg
            Client.Message.MessageParser par2 = new Client.Message.MessageParser();
            Client.MessageClasses.MsgUpload reup = par2.uploadParseMsg(resp);
            //Console.WriteLine("file path: {0}, container uri: {1}",
              //                 reup.filePathInSynFolder, reup.fileContainerUri);
            //9 Client upload
            new Client.UploadFunctions.UploadFile().UploadFileWithContainerUri(reup.fileContainerUri,fullpathOfChnagedFile , reup.filePathInSynFolder, md5r, time);
            
        }
    }
}
