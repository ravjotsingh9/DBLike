﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message
{
    public partial class MessageParser
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string filePathInSynFolder { get; set; }
        public DateTime fileTimeStamps { get; set; }
        public string fileHashValue { get; set; }
        public string fileName { get; set; }
        /**
        /// <summary>
        /// parse protocol
        /// +----------------------------------------------------------------------------------------------------------------------------------------------------+
        /// |upload|:|userName|:|password|:|File 1 path|:|File 1 Hash Value|:|File 1 Timestamp|:|...|:|File N path|:|File N Hash Value|:|File N Timestamp|:|<EOF>|
        /// +----------------------------------------------------------------------------------------------------------------------------------------------------+
        /// </summary>
        /// <param name="words"></param>
        public void uploadParseMsg(string[] words)
        {
            userName = words[1];
            password = words[2];
            string w= words[3];
            int i = 3;
            while(w!="<EOF>"){
                filePathInSynFolder.Add(words[i]);
                fileHashValue.Add(words[i + 1]);
                // String to DateTime
                DateTime MyDateTime = new DateTime();
                MyDateTime = DateTime.ParseExact(words[i + 2], "MM/dd/yyyy HH:mm:ss",
                                                 null);
                fileTimeStamps.Add(MyDateTime);
                i++;
                w = words[i];
            }

        }
      **/

        /// <summary>
        /// parse protocol
        /// +---------------------------------------------------------------------------------+
        /// |upload:<userName>:<password>:<File path>:<File Hash Value>:<File Timestamp>:<EOF>|
        /// +---------------------------------------------------------------------------------+
        /// </summary>
        /// <param name="words"></param>
        public void uploadParseMsg(string[] words)
        {
            userName = words[1];
            password = words[2];
           
           
            filePathInSynFolder = words[3];
            fileHashValue = words[4];
            // String to DateTime
            DateTime MyDateTime = new DateTime();
            
            MyDateTime = DateTime.ParseExact(words[5], "MM/dd/yyyy HH:mm:ss",
                                                null);
            fileTimeStamps = MyDateTime;
            string[] splitTogetFileName = filePathInSynFolder.Split('\\');
            fileName = splitTogetFileName[splitTogetFileName.Count() - 1];

        }
    }
}
