using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using Contracts;
using Service;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Server.Helpers;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private IHostingEnvironment _env;
        private IParallel_Manager _mpiManager;
        private IProcess_Manager _processManager;
        private FileHelper _fileHelper;

        public TasksController(IHostingEnvironment env)
        {
            _env = env;
            _mpiManager = new Parallel_Manager();
            _processManager = new Process_Manager();
            _fileHelper = new FileHelper(env.WebRootPath);
        }

        [HttpPost("CreateTask/{taskName}")]
        public async Task<bool> CreateTask(string taskName, IFormFileCollection files)
        {
            try
            {
                var webRoot = _env.WebRootPath;
                _fileHelper.CreateDirectory(taskName);
                string file = "";
                for (int i = 0; i < files.Count; i++)
                {
                    if (i == 0)
                        file = _fileHelper.CreateFile(taskName, "(EXECUTE){files[i].FileName}");//create mark that this file is exe file
                    else
                        file = _fileHelper.CreateFile(taskName, "{files[i].FileName}");
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await files[i].CopyToAsync(fileStream);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("StartTask/{taskName}/{threadCount}")]
        public void StartTask(string taskName, int threadCount)
        {

            var files = _fileHelper.GetFiles(taskName);
            var exeFile = files.Find(_ => _.Contains("(EXECUTE)"));
            _mpiManager.StartParallelProcess(exeFile, threadCount);
        }

        [HttpGet("StopTask/{taskName}")]
        public bool StopTask(string taskName)
        {
            try
            {
                var files = _fileHelper.GetFiles(taskName);

                _processManager.KillProcess(files);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("GetTaskStatus/{taskName}")]
        public bool GetTaskStatus(string taskName)
        {
            var files = _fileHelper.GetFiles(taskName);

            return _processManager.IsRunningProcess(files);
        }

        [HttpDelete("DeleteTask/{taskName}")]
        public bool DeleteTask(string taskName)
        {
            try
            {
                _fileHelper.DeleteDirectory(taskName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
