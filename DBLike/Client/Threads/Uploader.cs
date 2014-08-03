using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Client.LocalDbAccess;
using Client.MessageClasses;
using System.Windows.Forms;

namespace Client.Threads
{
    class Uploader
    {
        static Configuration.config conf = new Configuration.config();
        static Thread thread;
        public void start(string path, string eventType, string addiInfo, DateTime timestamp)
        {
            //TBD
            thread = new Thread(() => threadStartFun(path, eventType, addiInfo, timestamp));
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string fullpathOfChnagedFile, string eventType, string addiInfo, DateTime timestamp)
        {
            try
            {


                //System.Windows.Forms.MessageBox.Show("Started Uploader for " + fullpathOfChnagedFile, "Client");
                //TBD
                //send msg to server
                //get the information from the localDatabase
                LocalDB readLocalDB = new LocalDB();
                readLocalDB = readLocalDB.readfromfile();

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
                    DateTime time = att.lastModified.ToUniversalTime();
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
                    if (reup.indicator == "OK")
                    {
                        new Client.UploadFunctions.UploadFile().UploadFileWithContainerUri(reup.fileContainerUri, fullpathOfChnagedFile, reup.filePathInSynFolder, md5r, time, eventType);
                        System.Windows.Forms.MessageBox.Show(string.Format("Uploaded! \n event type: {0} \n Path: {1}", reup.addiInfo, fullpathOfChnagedFile), "DBLike Client");
                    }
                    // handle simultaneous editing confilct
                    if (reup.indicator == "simultaneousEditConfilct")
                    {

                        string tMsg = "simultaneous editing confilct";
                        string cMsg = "A newer version has been detected on the server.\nDo you want to save current version?\nYes to Save current file in another name\nNo to Download the newest version file from server";
                        DialogResult dialogResult = MessageBox.Show(cMsg, tMsg, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {

                            string sourcePath = fullpathOfChnagedFile;
                            string targetPath = "";
                            
                            // add time to new name
                            string dateString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                            string[] fileNameArr = sourcePath.Split('.');
                            string newFileName = fileNameArr[0] + " ( conflict copy " + dateString + ")." + fileNameArr[1];
                            targetPath = newFileName;

                            //// To copy a folder's contents to a new location:
                            //// Create a new target folder, if necessary.
                            //if (!System.IO.Directory.Exists(targetPath))
                            //{
                            //    System.IO.Directory.CreateDirectory(targetPath);
                            //}

                            // To copy a file to another location and 
                            // overwrite the destination file if it already exists.
                            System.IO.File.Copy(sourcePath, targetPath, true);





                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                        }

                    }
                   

                }

                else if (eventType == "delete")
                {
                    additionalInfo = "delete";
                    string msg = " ";
                    //System.Windows.Forms.MessageBox.Show(string.Format("Deletion detected!\n Path: {0}", pathInSyncFolderPath), "DBLike Client");


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

                    string[] separators = { "<", ">:<", ">" };
                    string[] reNameInfo = addiInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    //List<string> temp = reNameInfo.ToList();
                    //temp.Insert(0, "rename");

                    //System.Windows.Forms.MessageBox.Show(string.Format("Rename detected!\n + Old Path: {0} \n + New Path: {1}", reNameInfo[0], reNameInfo[1]), "DBLike Client");

                    // get new path in server
                    string[] pathName3 = reNameInfo[1].Split('\\');
                    int m = pathName3.Count();
                    string newPathInSyncFolderPath = "";
                    for (int n = pathName.Count(); n < m; n++)
                    {
                        newPathInSyncFolderPath += pathName3[n];
                        if ((n + 1) < m)
                        {
                            newPathInSyncFolderPath += "\\";
                        }
                    }

                    // send event type and new path to server
                    additionalInfo = "rename" + "|||" + newPathInSyncFolderPath;

                    // only change last modified time to current time
                    // Windows 8 won't change last modified time when only renaming
                    Client.LocalFileSysAccess.getFileAttributes att = new Client.LocalFileSysAccess.getFileAttributes(reNameInfo[1]);
                    DateTime renameTime = DateTime.UtcNow;

                    string msg;
                    string md5r = att.md5Value;

                    // create the msg
                    // pathInSyncFolderPath is the older path in server
                    // renameTime is the newest time
                    msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, renameTime, md5r, additionalInfo);

                    //send the msg using socket
                    ConnectionManager.Connection conn = new ConnectionManager.Connection();
                    Socket soc = conn.connect(conf.serverAddr, conf.port);

                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, msg);

                    //receive the msg
                    string resp = rw.readfromSocket(soc);

                }






            }
            catch (Exception ex)
            {
                if (eventType != "delete")
                {
                    fileBeingUsed.eventDetails e = new fileBeingUsed.eventDetails();
                    e.eventType = eventType;
                    e.datetime = timestamp;
                    e.filepath = fullpathOfChnagedFile;
                    Client.Program.filesInUse.removefromList(e);
                    
                }
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Client");
                //System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                if (eventType != "delete")
                {
                    fileBeingUsed.eventDetails e = new fileBeingUsed.eventDetails();
                    e.eventType = eventType;
                    e.datetime = timestamp;
                    e.filepath = fullpathOfChnagedFile;
                    Client.Program.filesInUse.removefromList(e);
                }
                Thread.CurrentThread.Abort();
            }
        }
    }
}
