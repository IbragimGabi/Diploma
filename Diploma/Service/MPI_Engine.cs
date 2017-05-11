using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Service
{
    public class MPI_Engine : IMPI_Engine
    {
        public TimeSpan? StartMPIProcess(string file, int threadCount)
        {
            Stopwatch stopWatch = new Stopwatch();
            int index = file.LastIndexOf(@"\");
            string dir = file.Substring(0, index + 1);
            int? result = null;

            stopWatch.Start();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                result = ExecuteWindowsCommand($"cd {dir} & mpiexec -n {threadCount} {file}");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                result = ExecuteLinuxCommand($"cd {dir} ; mpiexec -n {threadCount} {file}");
            stopWatch.Stop();

            if (result != null)
            {
                TimeSpan ts = stopWatch.Elapsed;
                return ts;
            }
            return null;
        }

        public TimeSpan? StartProcessByInterface(string file, int threadCount)
        {
            return null;
        }

        private int? ExecuteLinuxCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo(command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            process = Process.Start(processInfo);
            process.WaitForExit();

            exitCode = process.ExitCode;

            try
            {
                process.Kill();
                return exitCode;
            }
            catch
            {
                return null;
            }
        }

        private int? ExecuteWindowsCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            process = Process.Start(processInfo);
            process.WaitForExit();

            exitCode = process.ExitCode;

            try
            {
                process.Kill();
                return exitCode;
            }
            catch
            {
                return null;
            }
        }
    }
}