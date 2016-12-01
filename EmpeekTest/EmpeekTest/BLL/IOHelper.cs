using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmpeekTest.Models;

namespace EmpeekTest.BLL
{
    public static class IOHelper
    {
        public static BaseModel GetAllLocalDisks()
        {
            var driverslist = new List<Models.ObjectInfo>();
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                driverslist.Add(new Models.ObjectInfo
                {
                    Name = drive.ToString(),
                    FullPath = drive.Name,
                    Description = string.Format("({0})", drive.DriveType == DriveType.Fixed ? "Local disk" : drive.DriveType.ToString())
                });
            }

            return new BaseModel { CurrentPath = "My Computer", ObjectsList = driverslist };
        }

        public static BaseModel GetAllFilesInFolder(DirectoryInfo dirInfo)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            var filesModelList = new List<Models.ObjectInfo>();

            filesModelList.Add(new Models.ObjectInfo
            {
                FullPath = dirInfo.Parent != null ? dirInfo.Parent.FullName.ToString() : "",
                Name = "..",
            });

            try
            {
                subDirs = dirInfo.GetDirectories("*.*");
                files = dirInfo.GetFiles("*.*");
            }
            catch (Exception ex)
            {
            }

            if (subDirs != null)
                foreach (var dir in subDirs)
                {
                    filesModelList.Add(new Models.ObjectInfo
                    {
                        FullPath = dir.FullName.ToString(),
                        SpecialClass = dir.Attributes.HasFlag(FileAttributes.Hidden) ? "link-dark" : "",
                        Description = dir.Attributes.HasFlag(FileAttributes.System) ? "(system)" : "",
                        Name = dir.Name
                    });
                }

            if (files != null)
                foreach (var file in files)
                {
                    filesModelList.Add(new Models.ObjectInfo
                    {
                        FullPath = file.Directory.ToString(),//Для обычных файлов линк для перехода указываем текущую папку
                        Name = file.Name
                    });
                }

            return new BaseModel { ObjectsList = filesModelList, CurrentPath = dirInfo.FullName };
        }        

        public static FilesCountModel WalkThroughTree(DirectoryInfo path, FilesCountModel model)
        {          
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            try
            {
                files = path.GetFiles("*.*");
            }
            catch (Exception ex)
            {
            }

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    string fileFullName = file.FullName;
                    double fileSize = Convert.ToDouble(file.Length / 1024 / 1024);//B->KB->MB
                    if (fileSize > 100)
                        model.more++;
                    else if (fileSize < 10)
                        model.less++;
                    else model.between++;
                }
                subDirs = path.GetDirectories();
                foreach (var dirInfo in subDirs)
                {
                    WalkThroughTree(dirInfo, model);
                }
            }

            return model;
        }
    }
}