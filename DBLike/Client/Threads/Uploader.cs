using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Client.LocalDbAccess;

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
            try
            {

            
            //System.Windows.Forms.MessageBox.Show("Started Uploader", "Client");
            //TBD
            //send msg to server
            //get the information from the localDatabase
            LocalDB readLocalDB = new LocalDB();
            readLocalDB.readfromfile();
            string clientSyncFolderPath= readLocalDB.getPath();
            string[] pathName = clientSyncFolderPath.Split('\\');
            string[] pathName2 = fullpathOfChnagedFile.Split('\\');
            int i = pathName2.Count();
            string pathInSyncFolderPath = "";
            for (int j = pathName.Count(); j < i; j++)
            {
                pathInSyncFolderPath += pathName2[j];
                if ((j + 1) < i)
                {
                    pathInSyncFolderPath += "\\";
                }   
            }
            string userName = readLocalDB.getUsername();
            string password = readLocalDB.getPassword();
            Client.Message.CreateMsg uploadM = new Client.Message.CreateMsg();
            Client.LocalFileSysAccess.getFileAttributes att = new Client.LocalFileSysAccess.getFileAttributes(fullpathOfChnagedFile);
            DateTime time = att.lastModified;
            string msg;
            string md5r = att.md5Value;
            msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, time, md5r);
     
              
            
           
            

            //send the msg using socket
            ConnectionManager.Connection conn = new ConnectionManager.Connection();
            Socket soc = conn.connect(conf.serverAddr, conf.port);

            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            rw.writetoSocket(soc, msg);
            //receive the msg
            string resp = rw.readfromSocket(soc);
            
            //8 Client parse msg
            Client.Message.MessageParser par2 = new Client.Message.MessageParser();
            Client.MessageClasses.MsgRespUpload reup = par2.uploadParseMsg(resp);
            //Console.WriteLine("file path: {0}, container uri: {1}",
              //                 reup.filePathInSynFolder, reup.fileContainerUri);
            //9 Client upload
            new Client.UploadFunctions.UploadFile().UploadFileWithContainerUri(reup.fileContainerUri,fullpathOfChnagedFile , reup.filePathInSynFolder, md5r, time);
            //System.Windows.Forms.MessageBox.Show("Uploaded!!!", "Client");
            
            }
            catch(Exception e){
                System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}
