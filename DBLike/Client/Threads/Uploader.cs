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
        public void start(string path, string eventType)
        {
            //TBD
            thread = new Thread(() => threadStartFun(path, eventType));
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string fullpathOfChnagedFile, string eventType)
        {
            try
            {


                //System.Windows.Forms.MessageBox.Show("Started Uploader", "Client");
                //TBD
                //send msg to server
                //get the information from the localDatabase
                LocalDB readLocalDB = new LocalDB();
                readLocalDB.readfromfile();

                string clientSyncFolderPath = readLocalDB.getPath();
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
                string additionalInfo = "";

                if (eventType == "create" || eventType == "change" || eventType == "signUpStart")
                {
                    if (eventType == "create")
                    {
                        additionalInfo = "create";
                    }
                    if (eventType == "change")
                    {
                        additionalInfo = "change";
                    }
                    if (eventType == "signUpStart")
                    {
                        additionalInfo = "signUpStart";
                    }

                    // belong to these events because for delete event it won't get the attributes anymore
                    Client.LocalFileSysAccess.getFileAttributes att = new Client.LocalFileSysAccess.getFileAttributes(fullpathOfChnagedFile);
                    DateTime time = att.lastModified;
                    string msg;
                    string md5r = att.md5Value;

                    // create the msg
                    msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, time, md5r, additionalInfo);

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

                    //9 Client upload                  
                    new Client.UploadFunctions.UploadFile().UploadFileWithContainerUri(reup.fileContainerUri, fullpathOfChnagedFile, reup.filePathInSynFolder, md5r, time);
                    System.Windows.Forms.MessageBox.Show(string.Format("Uploaded! \n event type: {0} \n Path: {1}", reup.addiInfo, fullpathOfChnagedFile), "DBLike Client");

                }

                else if (eventType == "delete")
                {
                    additionalInfo = "delete";
                    string msg = " ";
                    System.Windows.Forms.MessageBox.Show(string.Format("Deletion detected!\n Path: {0}", pathInSyncFolderPath), "DBLike Client");


                    // create the msg
                    // use " " instead of null to avoid parsing issue
                    // use DateTime.MinValue to avoid parsing issue
                    msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, DateTime.MinValue, " ", additionalInfo);

                    //send the msg using socket
                    ConnectionManager.Connection conn = new ConnectionManager.Connection();
                    Socket soc = conn.connect(conf.serverAddr, conf.port);

                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, msg);


                }
                else if (eventType == "rename")
                {
                    additionalInfo = "rename";
                }






            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                //System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}
