using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Contracts;

namespace Service
{
    public class MPI_Engine : IParallel_Engine
    {
        public Process StartParallelProcess(string file, int threadCount)
        {
            int index = file.LastIndexOf(@"\");
            string dir = file.Substring(0, index + 1);
            Process result = null;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                result = ExecuteWindowsCommand($"cd {dir} & mpiexec -n {threadCount} {file}");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                result = ExecuteLinuxCommand($"cd {dir} ; mpiexec -n {threadCount} {file}");

            return result;
        }

        private Process ExecuteLinuxCommand(string command)
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo(command)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            process = Process.Start(processInfo);
            process.WaitForExit();

            try
            {
                process.Kill();
                return process;
            }
            catch
            {
                return null;
            }
        }

        private Process ExecuteWindowsCommand(string command)
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            process = Process.Start(processInfo);
            process.WaitForExit();

            try
            {
                process.Kill();
                return process;
            }
            catch
            {
                return null;
            }
        }
    }
}