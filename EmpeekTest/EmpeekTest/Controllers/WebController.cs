using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EmpeekTest.Models;
using EmpeekTest.BLL;


namespace EmpeekTest.Controllers
{
    public class WebController : ApiController
    {
        public BaseModel GetObjectsInCurrentFolder(string pathToFile)
        {
            return string.IsNullOrEmpty(pathToFile) ? IOHelper.GetAllLocalDisks() : IOHelper.GetAllFilesInFolder(new DirectoryInfo(pathToFile));
        }

        public FilesCountModel GetFilesCountInCurrentFolder(string pathToFile)
        {
            return string.IsNullOrEmpty(pathToFile) ? new FilesCountModel() : IOHelper.WalkThroughTree(new DirectoryInfo(pathToFile), new FilesCountModel());
        }
    }
}