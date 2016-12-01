using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EmpeekTest.Models
{
    public class BaseModel
    {
        public List<ObjectInfo> ObjectsList { get; set; }
        public string CurrentPath { get; set; } 
    }

    public class FilesCountModel
    {
        public int less;
        public int between;
        public int more;

        public FilesCountModel()
        {
            less = between = more = 0;
        }
    }

    public class ObjectInfo
    {
        public string Description { get; set; }
        public string SpecialClass { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }        
    }
}