using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Client.Threads
{
    class FileSysWatchDog
    {
        
        static Thread installWatcher = new Thread(() => Run());
        static FileSystemWatcher watcher;
        

        
        public void start()
        {
            installWatcher.Start();
        //   btnclicked.Start();
        //   return true;
        /*
        string[] args = new string[3];
        LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
        args =file.readfromfile();
        if (args[2].Equals(""))
        {
            MessageBox.Show("Got problem in finding local sync folder", "Could not find local sync folder");
            return false;
        }
        else
        {
            btnclicked = new Thread(() => Run(args[2]));
            btnclicked.Start();
            return true;
        }
       */
        }

        public void stop()
        {
            if (watcher.EnableRaisingEvents == true)
            {
                watcher.EnableRaisingEvents = false;
            }
            //btnclicked.Abort();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
            file = file.readfromfile();
            if (file.getPath().Equals(""))
            {
                MessageBox.Show("Got problem in finding local sync folder", "Could not find local sync folder");
                Thread.CurrentThread.Abort();
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("started", "FileWatchdog Thread started");
                //"C:\\Users\\Owner\\Desktop\\Term2_Desktop";
                watcher = new FileSystemWatcher();
                watcher.Path = file.getPath();
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName; //| NotifyFilters.DirectoryName;
                watcher.IncludeSubdirectories = true;
                // Add event handlers.
                watcher.Changed += new FileSystemEventHandler(OnChanged); //change
                watcher.Created += new FileSystemEventHandler(OnChanged); //creation
                //watcher.Created += new FileSystemEventHandler(OnCreated); //creation
                watcher.Deleted += new FileSystemEventHandler(OnDeleted); //deletion
                watcher.Renamed += new RenamedEventHandler(OnRenamed);    //renaming
                // Start watching.
                watcher.EnableRaisingEvents = true;
                //MessageBox.Show("Event handler Installed", "Client");
            }
            Thread.CurrentThread.Abort();
        }

        

        // Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            string eventType;
            /*
            if (onchnageToggler==true)
            {
                onchnageToggler = false;
            }
            else
            {
                onchnageToggler = true;
                return;
            }
            */
            //MessageBox.Show("OnChanged Event Raised", "Client");
            //Thread.Sleep(1000);
            try
            {
                //watcher.EnableRaisingEvents = false;
                //MessageBox.Show("OnChangedFun : File: " + e.FullPath + " " + e.ChangeType);
                if(File.Exists(e.FullPath))
                {

                    // let it sleep to avoid multiple "change" detection
                    // b/c IO is much slower than thread!
                    // but for large files, this needs to be patched
                    //Thread.Sleep(500);
                    if (e.ChangeType == WatcherChangeTypes.Changed)
                    {
                        eventType = "change";
                    }
                    else
                    {
                        eventType = "create";
                    }

                    LocalFileSysAccess.getFileAttributes timestamp = new LocalFileSysAccess.getFileAttributes(e.FullPath);
                    fileBeingUsed.eventDetails eventdet = new fileBeingUsed.eventDetails();
                    eventdet.datetime = timestamp.lastModified;
                    eventdet.filepath = e.FullPath;
                    eventdet.eventType = eventType;
                    if(Client.Program.filesInUse.alreadyPresent(eventdet))
                    {
                        return;
                    }
                    else
                    {
                        Client.Program.filesInUse.addToList(eventdet);
                    }

                    Uploader upload = new Uploader();
                    //if (e.ChangeType == WatcherChangeTypes.Changed)
                    //{
                        upload.start(e.FullPath, eventType, null, timestamp.lastModified);
                    //}
                    //else
                    //{
                      // upload.start(e.FullPath, "create", null);
                    //}
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }
                
            }
            finally
            {
                //watcher.EnableRaisingEvents = true;
            }

        }
        /*
        // Define the event handlers. 
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
         
            //MessageBox.Show("OnCreatedFun: File: " + e.FullPath + " " + e.ChangeType);
            //MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
            try
            {
                //watcher.EnableRaisingEvents = false;
                if (File.Exists(e.FullPath))
                {
                    Uploader upload = new Uploader();
                    upload.start(e.FullPath, "create", null);
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
            
        }
         */ 
        // Define the event handlers. 
        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show("OnDeleted Event Raised", "Client");
            //MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
            /*
            string eventType = "delete";
            LocalFileSysAccess.getFileAttributes timestamp = new LocalFileSysAccess.getFileAttributes(e.FullPath);
            fileBeingUsed.eventDetails eventdet = new fileBeingUsed.eventDetails();
            eventdet.datetime = timestamp.lastModified;
            eventdet.filepath = e.FullPath;
            eventdet.eventType = eventType;
            if (Client.Program.filesInUse.alreadyPresent(eventdet))
            {
                return;
            }
            else
            {
                Client.Program.filesInUse.addToList(eventdet);
            }
             */ 
            Uploader upload = new Uploader();
            upload.start(e.FullPath, "delete", null,DateTime.Now);

        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            //MessageBox.Show("OnRenameed Event Raised", "Client");
            //MessageBox.Show("File: " + e.OldFullPath + " renamed to " + e.FullPath);
            string eventType = "rename";
            LocalFileSysAccess.getFileAttributes timestamp = new LocalFileSysAccess.getFileAttributes(e.FullPath);
            fileBeingUsed.eventDetails eventdet = new fileBeingUsed.eventDetails();
            eventdet.datetime = timestamp.lastModified;
            eventdet.filepath = e.FullPath;
            eventdet.eventType = eventType;
            if (Client.Program.filesInUse.alreadyPresent(eventdet))
            {
                return;
            }
            else
            {
                Client.Program.filesInUse.addToList(eventdet);
            }
            //Client.MessageClasses.changedFile renamedFile = new Client.MessageClasses.changedFile(e.OldFullPath, e.FullPath, e.OldName, e.Name);
            string renamedStr = "<" + e.OldFullPath + ">:<" + e.FullPath + ">:<" + e.OldName + ">:<" + e.Name + ">";


            Uploader upload = new Uploader();
            upload.start(e.OldFullPath, "rename", renamedStr,timestamp.lastModified);

        }





        /*
        private void servicestart()
        {
            DateTime lastedited = File.GetLastWriteTime(@"textDoc.txt");
            while (true)
            {
                if (lastedited < File.GetLastWriteTime(@"textDoc.txt"))
                {
                    lastedited = File.GetLastWriteTime(@"textDoc.txt");
                    MessageBox.Show("filechanged!");
                    break;
                }
            }
            btnclicked.Abort();
        }
         */
    }
}
