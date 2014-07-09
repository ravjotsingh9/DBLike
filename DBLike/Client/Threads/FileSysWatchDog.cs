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
    class FileSysWatchDog
    {
        Thread btnclicked;
        public FileSysWatchDog()
        {
            
        }

        public bool start()
        {
            //btnclicked = new Thread(new ThreadStart(servicestart));
            FolderBrowserDialog arg = new FolderBrowserDialog();
            //arg.ShowDialog();
            string[] args;
            LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
            args =file.readfromfile();
            if (args[2].Equals(""))
            {
                btnclicked = new Thread(() => Run(args[2]));
                btnclicked.Start();
                return true;
            }
            else
            {
                MessageBox.Show("Got problem in finding local sync folder","Could not find local sync folder");
                return false;
            }
            
        }
        public void stop()
        {
            btnclicked.Abort();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run(string args)
        {
            System.Windows.Forms.MessageBox.Show("started", "FileWatchdog Thread started");
            //"C:\\Users\\Owner\\Desktop\\Term2_Desktop";
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args;   
            watcher.NotifyFilter =  NotifyFilters.LastWrite| NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.IncludeSubdirectories = true;
            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged); //change
            watcher.Created += new FileSystemEventHandler(OnChanged); //creation
            watcher.Deleted += new FileSystemEventHandler(OnChanged); //deletion
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);    //renaming
            // Start watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
            Uploader upload = new Uploader();
            upload.start(e.FullPath);
            //upload.stop();
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            MessageBox.Show("File: " + e.OldFullPath + " renamed to " + e.FullPath);
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
