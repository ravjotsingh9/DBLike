﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        
        static public Client.Threads.PollFiles poll = new Threads.PollFiles();
        static public Client.Threads.FileSysWatchDog folderWatcher = new Threads.FileSysWatchDog();
        static public Client.Threads.fileBeingUsed filesInUse = new Threads.fileBeingUsed();

        static public bool exit = true;
        static public Form1 ClientForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Configuration.flag.polling = false;
            
            if(System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("Sorry for Interrupt, but I guess the application is already running! :-)","Application Already Running");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            ClientForm = new Form1();
            //Program.ClientForm.Closed += new EventHandler(ClientForm.OnFormClosed);
            //Program.ClientForm.Closing += new CancelEventHandler(ClientForm.OnFormClosed); 
            Application.Run(ClientForm);
            
            
        }
        
    }
}
