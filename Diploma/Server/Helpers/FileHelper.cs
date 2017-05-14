﻿using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public class FileHelper
    {
        private string _webRoot;

        public FileHelper(string webRoot)
        {
            _webRoot = webRoot;
        }

        public List<string> GetFiles(string taskName)
        {
            if (Directory.Exists($"{_webRoot}\\Files\\{taskName}"))
                return new List<string>(Directory.EnumerateFiles($"{_webRoot}\\Files\\{taskName}"));
            else
                throw new Exception();
        }

        public void CreateDirectory(string taskName)
        {
            if (!Directory.Exists($"{_webRoot}\\Files\\{taskName}"))
                Directory.CreateDirectory($"{_webRoot}\\Files\\{taskName}");
            else
                throw new Exception();
        }

        public void DeleteDirectory(string taskName)
        {
            var taskPath = $"{_webRoot}\\Files\\{taskName}";
            if (Directory.Exists(taskPath))
            {
                DirectoryInfo di = new DirectoryInfo(taskPath);

                di.GetFiles().ToList().ForEach(_ => _.Delete());
                di.GetDirectories().ToList().ForEach(_ => _.Delete(true));

                Directory.Delete(taskPath);
            }
            else
                throw new Exception();
        }

        public string CreateFile(string taskName, string file)
        {
            return $"{_webRoot}\\Files\\{taskName}\\{file}";
        }

    }
}
