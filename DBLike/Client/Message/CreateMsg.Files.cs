﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Message
{
    public partial class CreateMsg
    {
        /**
       /// <summary>
       /// upload protocol
       /// +----------------------------------------------------------------------------------------------------------------------------------------------------+
       /// |upload|:|userName|:|password|:|File 1 path|:|File 1 Hash Value|:|File 1 Timestamp|:|...|:|File N path|:|File N Hash Value|:|File N Timestamp|:|<EOF>|
       /// +----------------------------------------------------------------------------------------------------------------------------------------------------+
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="passWord"></param>
       /// <param name="filePathInSynFolder"></param>
       /// <param name="timeStamps"></param>
       /// <param name="fileHashValue"></param>
       /// <returns></returns>

       public string uploadMsg(string userName, string passWord, List<string> filePathInSynFolder, List<DateTime> fileTimeStamps, List<string> fileHashValue)
       {
           string msg = "upload:" + userName + ":" + passWord + ":";
           int count = filePathInSynFolder.Count();
           for (int i = 0; i < count; i++)
           {
               msg += filePathInSynFolder[i] + ":";
               msg += fileHashValue[i] + ":";
               msg += fileTimeStamps[i].ToString("MM/dd/yyyy HH:mm:ss") + ":";

           }

           msg += "<EOF>";

           return msg;
       }
     **/

        /// <summary>
        /// upload protocol
        /// +---------------------------------------------------------------------------------+
        /// |upload:<userName>:<password>:<File path in sync folder>:<File Hash Value>:<File Timestamp>:<EOF>|
        /// +---------------------------------------------------------------------------------+
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="filePathInSynFolder"></param>
        /// <param name="timeStamps"></param>
        /// <param name="fileHashValue"></param>
        /// <returns></returns>

        // add addtionalIno for deletetion and renaming
        public string uploadMsg(string userName, string passWord, string filePathInSynFolder,
            DateTime fileTimeStamps, string fileHashValue, string additionalInfo)
        {
            string msg = "<UPLOAD>:<" + userName + ">:<" + passWord + ">:<";


            msg += filePathInSynFolder + ">:<";
            msg += fileHashValue + ">:<";

            msg += fileTimeStamps.ToString("MM/dd/yyyy HH:mm:ss") + ">:<";
            msg += additionalInfo + ">:";


            msg += "<EOF>";

            return msg;
        }


    }
}
