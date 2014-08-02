using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Client.LocalFileSysAccess
{

    // for every file
    public class FileInfo
    {
        public DateTime time;
        public string md5r;
    }



    // local file list to store hashvalue and time stamp
    public static class FileList
    {
        //public string fullpathOfChnagedFile;
        public static ConcurrentDictionary<string, FileInfo> fileInfoDic;


        // constructor
        static FileList()
        {
            // We know how many items we want to insert into the ConcurrentDictionary. 
            // So set the initial capacity to some prime number above that, to ensure that 
            // the ConcurrentDictionary does not need to be resized while initializing it. 
            int initialCapacity = 1024;

            // The higher the concurrencyLevel, the higher the theoretical number of operations 
            // that could be performed concurrently on the ConcurrentDictionary.  However, global 
            // operations like resizing the dictionary take longer as the concurrencyLevel rises.  
            // For the purposes of this example, we'll compromise at numCores * 2. 
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;


            // Construct the dictionary with the desired concurrencyLevel and initialCapacity
            // dic to store file timestamp and hashvalue
            fileInfoDic = new ConcurrentDictionary<string, FileInfo>(concurrencyLevel, initialCapacity);

        }




    }
}
