using Server.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        DBConnection test;
        static void Main()
        {
            
       
            
            try
            {
               test = new DBConnection(1);
               test.DBConnect();
               Console.WriteLine("yy");
               Console.ReadLine();
            }
            catch
            {

            }
            finally
            {
                test.DBClose();
            }
            
        
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
