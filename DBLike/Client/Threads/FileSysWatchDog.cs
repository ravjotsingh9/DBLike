using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Threads
{
    static class FileSysWatchDog
    {
        //static Thread btnclicked = new Thread(() => Run());
        static FileSystemWatcher watcher;


        //public bool start()
        // {

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
        //}

        public static void stop()
        {
            watcher.EnableRaisingEvents = false;
            //btnclicked.Abort();

        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            //string[] args = new string[3];

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
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.IncludeSubdirectories = true;
                // Add event handlers.
                watcher.Changed += new FileSystemEventHandler(OnChanged); //change
                watcher.Created += new FileSystemEventHandler(OnCreated); //creation
                watcher.Deleted += new FileSystemEventHandler(OnDeleted); //deletion
                watcher.Renamed += new RenamedEventHandler(OnRenamed);    //renaming
                // Start watching.
                watcher.EnableRaisingEvents = true;
                //MessageBox.Show("Event handler Installed", "Client");
            }
        }

        // Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show("OnChanged Event Raised", "Client");
            //Thread.Sleep(1000);
            try
            {
                watcher.EnableRaisingEvents = false;
                //MessageBox.Show("OnChangedFun : File: " + e.FullPath + " " + e.ChangeType);

                Uploader upload = new Uploader();
                upload.start(e.FullPath, "change", null);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }

        }

        // Define the event handlers. 
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show("OnCreated Event Raised", "Client");
            //Thread.Sleep(1000);
            //MessageBox.Show("OnCreatedFun: File: " + e.FullPath + " " + e.ChangeType);
            //MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
            Uploader upload = new Uploader();
            upload.start(e.FullPath, "create", null);

        }
        // Define the event handlers. 
        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show("OnDeleted Event Raised", "Client");
            //MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
            Uploader upload = new Uploader();
            upload.start(e.FullPath, "delete", null);

        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            //MessageBox.Show("OnRenameed Event Raised", "Client");
            //MessageBox.Show("File: " + e.OldFullPath + " renamed to " + e.FullPath);

            //Client.MessageClasses.changedFile renamedFile = new Client.MessageClasses.changedFile(e.OldFullPath, e.FullPath, e.OldName, e.Name);
            string renamedStr = "<" + e.OldFullPath + ">:<" + e.FullPath + ">:<" + e.OldName + ">:<" + e.Name + ">";


            Uploader upload = new Uploader();
            upload.start(e.OldFullPath, "rename", renamedStr);

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
