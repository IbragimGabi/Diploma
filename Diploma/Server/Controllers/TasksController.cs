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
        private IMPI_Manager _mpiManager;
        private FileHelper _fileHelper;
        private ProcessHelper _processHelper;

        public TasksController(IHostingEnvironment env)
        {
            _env = env;
            _mpiManager = new MPI_Manager();
            _fileHelper = new FileHelper(env.WebRootPath);
            _processHelper = new ProcessHelper();
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
        public bool StartTask(string taskName, int threadCount)
        {
            try
            {
                var files = _fileHelper.GetFiles(taskName);
                var exeFile = files.Find(_ => _.Contains("(EXECUTE)"));
                _mpiManager.StartMPIProcess(exeFile, threadCount);
                return true;
            }
            catch
            {
                return false;
            }
        }



        [HttpGet("StopTask/{taskName}")]
        public bool StopTask(string taskName)
        {
            try
            {
                var files = _fileHelper.GetFiles(taskName);

                _processHelper.FindProcess(files).ForEach(_ => _.Kill());
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

            return (_processHelper.FindProcess(files).Count > 0) ? true : false;
        }



        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
