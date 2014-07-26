using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageClasses
{
    public class MsgUpload
    {
        public string indicator { get; set; }
        public string filePathInSynFolder { get; set; }
        public string fileContainerUri { get; set; }
        public string fileBlobUri { get; set; }
        public string type { get; set; }
    }



    //public struct changedFile
    //{

    //    public string oldFullPath;
    //    public string newFullPath;
    //    public string oldName;
    //    public string newName;

    //    public changedFile(string oldFullPath, string newFullPath, string oldName, string newName)
    //    {
    //        this.oldFullPath = oldFullPath;
    //        this.newFullPath = newFullPath;
    //        this.oldName = oldName;
    //        this.newName = newName;
    //    }
    //}

}
