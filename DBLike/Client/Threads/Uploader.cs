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
using System.Globalization;

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

                Program.ClientForm.addtoConsole("Upload thread Started");
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
                        // add to the file list
                        Client.LocalFileSysAccess.FileListMaintain addToFileList = new Client.LocalFileSysAccess.FileListMaintain();
                        addToFileList.addSingleFileToFileList(fullpathOfChnagedFile);
                    }
                    if (eventType == "change")
                    {
                        additionalInfo = "change";
                    }
                    if (eventType == "signUpStart")
                    {
                        additionalInfo = "signUpStart";
                    }

                    Program.ClientForm.addtoConsole("Event handling:" + eventType);
                    // get the initial attribute before making this "change"
                    Client.LocalFileSysAccess.FileInfo tmp = new Client.LocalFileSysAccess.FileInfo();
                    Client.LocalFileSysAccess.FileList.fileInfoDic.TryGetValue(fullpathOfChnagedFile, out tmp);
                    DateTime timeBefore = tmp.time;
                    string md5rBefore = tmp.md5r;


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
                    Program.ClientForm.addtoConsole("Wrote to socket");
                    //receive the msg
                    string resp = rw.readfromSocket(soc);
                    Program.ClientForm.addtoConsole("Read from socket");
                    //8 Client parse msg
                    Client.Message.MessageParser par2 = new Client.Message.MessageParser();
                    if (resp != null)
                    {
                        Client.MessageClasses.MsgRespUpload reup = par2.uploadParseMsg(resp);
                    


                        //9 Client upload
                        if (reup.indicator == "OK")
                        {
                            // event type when there's no file conflict
                            string tempEType = reup.addiInfo;


                            string[] str = null;
                            // get current file info on server
                            string[] separators = { "|||" };
                            // when event type is change
                            // otherwise directly upload, don't need to check conflict
                            if (reup.addiInfo.Contains("|||"))
                            {
                                str = reup.addiInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                string currHashValueOnServer = str[0];
                                string tempTimestampOnServer = str[1];
                                // reassign event type
                                tempEType = str[2];

                                //CultureInfo provider = new CultureInfo("en-US");
                                //CultureInfo provider = CultureInfo.InvariantCulture;
                                //tempTimestampOnServer = tempTimestampOnServer.Replace("-", "/");
                                Program.ClientForm.addtoConsole(tempTimestampOnServer);
                                DateTime currTimestampOnServer = DateTime.Parse(tempTimestampOnServer);
                                //DateTime currTimestampOnServer = DateTime.ParseExact(tempTimestampOnServer, "MM/dd/yyyy HH:mm:ss", null);


                                // file has been changed during open time and save time
                                // aka there's a newer version of this file uploaded by another user during this time
                                if (DateTime.Compare(timeBefore, currTimestampOnServer) < 0 && String.Compare(md5rBefore, currHashValueOnServer) != 0)
                                {
                                    string tMsg = "simultaneous editing confilct";
                                    string cMsg = "\nA newer version has been detected on the server.\nDo you want to save current version?\n\nYes to Save current file in another name\nNo to Download the newest version file from server";
                                    DialogResult dialogResult = MessageBox.Show(string.Format("{0}" + cMsg, fullpathOfChnagedFile), tMsg, MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {

                                        string sourcePath = fullpathOfChnagedFile;
                                        string targetPath = "";

                                        // add time to new name
                                        string dateString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                                        //string dateString = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
                                        string[] fileNameArr = sourcePath.Split('.');
                                        string newFileName = fileNameArr[0] + " ( " + Environment.UserName + "'s conflict copy " + dateString + ")." + fileNameArr[1];
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


                                        // delete current copy
                                        // try delete while it still exists
                                        while (System.IO.File.Exists(fullpathOfChnagedFile))
                                        {
                                            // delete current file
                                            // won't delete blob file on the server
                                            // b/c it's older than that version
                                            System.IO.File.Delete(fullpathOfChnagedFile);

                                        }


                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {

                                        // try delete while it still exists
                                        while (System.IO.File.Exists(fullpathOfChnagedFile))
                                        {
                                            // delete current file
                                            // won't delete blob file on the server
                                            // b/c it's older than that version
                                            System.IO.File.Delete(fullpathOfChnagedFile);

                                        }

                                        // if file is in dic
                                        if (Client.LocalFileSysAccess.FileList.fileInfoDic.ContainsKey(fullpathOfChnagedFile))
                                        {
                                            // after it has been deleted
                                            // del from the file list
                                            Client.LocalFileSysAccess.FileListMaintain delFromFileList = new Client.LocalFileSysAccess.FileListMaintain();
                                            delFromFileList.removeSingleFileFromFileList(fullpathOfChnagedFile, time, md5r);

                                            // TODO
                                            // download newer version from server

                                        }
                                    }
                                }
           
                            }

                            new Client.UploadFunctions.UploadFile().UploadFileWithContainerUri(reup.fileContainerUri, fullpathOfChnagedFile, reup.filePathInSynFolder, md5r, time, eventType);
                            //System.Windows.Forms.MessageBox.Show(string.Format("Uploaded! \n event type: {0} \n Path: {1}", tempEType, fullpathOfChnagedFile), "DBLike Client");
                            Program.ClientForm.addtoConsole(string.Format("Uploaded! \n event type: {0} \n Path: {1}", tempEType, fullpathOfChnagedFile));
                            // update file list
                            Client.LocalFileSysAccess.FileListMaintain updateFileList = new Client.LocalFileSysAccess.FileListMaintain();
                            updateFileList.updateSingleFileToFileList(fullpathOfChnagedFile, time, md5r);

                        }
                        //// handle simultaneous editing confilct
                        //// currently client will handle this
                        //if (reup.indicator == "simultaneousEditConfilct")
                        //{
                        //}

                    }
                    else
                    {
                        Program.ClientForm.addtoConsole("Error : <<Response from server is null>>");
                    }
                }

                else if (eventType == "delete")
                {
                    Program.ClientForm.addtoConsole("Event handling:" + eventType);
                    additionalInfo = "delete";
                    string msg = " ";
                    //System.Windows.Forms.MessageBox.Show(string.Format("Deletion detected!\n Path: {0}", pathInSyncFolderPath), "DBLike Client");


                    // file list maintainence_1
                    // delete the file from the file list
                    // grab it first by key, then delete
                    Client.LocalFileSysAccess.FileListMaintain reFileList = new Client.LocalFileSysAccess.FileListMaintain();
                    // get value by key
                    Client.LocalFileSysAccess.FileInfo tmpFInfo = new Client.LocalFileSysAccess.FileInfo();
                    Client.LocalFileSysAccess.FileList.fileInfoDic.TryGetValue(fullpathOfChnagedFile, out tmpFInfo);


                    
                    // create the msg
                    // use " " instead of null to avoid parsing issue
                    // use DateTime.MinValue to avoid parsing issue
                    //msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, DateTime.MinValue, " ", additionalInfo);
                    msg = uploadM.uploadMsg(userName, password, pathInSyncFolderPath, tmpFInfo.time, " ", additionalInfo);


                    //send the msg using socket
                    ConnectionManager.Connection conn = new ConnectionManager.Connection();
                    Socket soc = conn.connect(conf.serverAddr, conf.port);

                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, msg);



                    // file list maintainence_2
                    // remove file from file list
                    reFileList.removeSingleFileFromFileList(fullpathOfChnagedFile, tmpFInfo.time, tmpFInfo.md5r);
                    Program.ClientForm.ballon("Deleted:"+ fullpathOfChnagedFile);


                }
                else if (eventType == "rename")
                {
                    Program.ClientForm.addtoConsole("Event handling:" + eventType);
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



                    // update the renamed file to the file list
                    Client.LocalFileSysAccess.FileListMaintain renameFileList = new Client.LocalFileSysAccess.FileListMaintain();

                    // get value by key
                    Client.LocalFileSysAccess.FileInfo tmpFI = new Client.LocalFileSysAccess.FileInfo();
                    Client.LocalFileSysAccess.FileList.fileInfoDic.TryGetValue(fullpathOfChnagedFile, out tmpFI);

                    // remove older file from file list
                    renameFileList.removeSingleFileFromFileList(fullpathOfChnagedFile, tmpFI.time, tmpFI.md5r);
                    // add new name file to file list
                    renameFileList.addSingleFileToFileList(reNameInfo[1]);



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
                    //string resp = rw.readfromSocket(soc);

                    Program.ClientForm.ballon("Rename:" + fullpathOfChnagedFile);



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
                Program.ClientForm.addtoConsole("Exception Occured:" + ex.ToString());
                //System.Windows.Forms.MessageBox.Show(ex.ToString(), "Client");
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
                Program.ClientForm.addtoConsole("Exiting");
                Thread.CurrentThread.Abort();
            }
        }
    }
}
