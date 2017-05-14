using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.StartMenu();
        }

        internal class Menu
        {
            bool _exitFlag;
            private Dictionary<int, string> _menu;
            private delegate void CallDelegate();

            public Menu()
            {
                _menu = new Dictionary<int, string>();
                _menu.Add(1, "Create task");
                _menu.Add(2, "Start task");
                _menu.Add(3, "Stop task");
                _menu.Add(4, "Get task's status");
                _menu.Add(5, "Delete task");
                _menu.Add(6, "Exit");
            }

            public void StartMenu()
            {
                Console.Clear();
                _exitFlag = false;
                while (!_exitFlag)
                {
                    ShowMenu();
                    var key = Console.ReadLine();
                    Console.Clear();
                    try
                    {
                        GetAssociatedDelegate(
                            Convert.ToInt32(key)).Invoke();
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                        Console.WriteLine(exp.StackTrace);
                    }

                    if (!_exitFlag)
                    {
                        Pause();
                        Console.Clear();
                    }
                }
            }

            public void ShowMenu()
            {
                Console.WriteLine("Choose command and write row number:");

                foreach (var item in _menu)
                {
                    Console.WriteLine(item.Key + ". " + item.Value);
                }
            }

            private void Pause()

            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }

            private CallDelegate GetAssociatedDelegate(int numberOfRow)
            {
                switch (numberOfRow)
                {
                    case 1:
                        return CreateTask;
                    case 2:
                        return StartTask;
                    case 3:
                        return StopTask;
                    case 4:
                        return GetStatus;
                    case 5:
                        return DeleteTask;
                    default:
                        return Exit;
                }
            }

            private void CreateTask()
            {
                Console.Clear();

                try
                {
                    var task = new TaskModel();
                    Console.WriteLine("Enter task name:");
                    task.TaskName = Console.ReadLine();
                    Console.WriteLine("Enter files absoulte paths splitted by ;");
                    var strFiles = Console.ReadLine();
                    var files = strFiles.Split(';');
                    foreach (var file in files)
                    {
                        task.Files.Add(file);
                    }
                    Console.WriteLine($"Task name: {task.TaskName}");
                    Console.WriteLine("Task files:");
                    task.Files.ForEach(_ => Console.WriteLine(_));

                    var result = Proxy.CreateTaskRequest(task);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        Console.WriteLine("Task created!");
                    else
                        Console.WriteLine("Something wrong!");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void StartTask()
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Enter task name:");
                    var task = Console.ReadLine();
                    Console.WriteLine("Enter thread count");
                    var threadCount = Convert.ToInt32(Console.ReadLine());

                    Proxy.StartTaskRequest(task, threadCount);
                    Console.WriteLine($"Task {task} started");
                    //if (result == "true")
                    //    Console.WriteLine($"Task {task} started");
                    //else
                    //    Console.WriteLine($"Task {task} not started");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void StopTask()
            {
                Console.Clear();

                try
                {
                    Console.WriteLine("Enter task name:");
                    var task = Console.ReadLine();
                    var result = Proxy.StopTaskRequest(task).Result;
                    if (result == "true")
                        Console.WriteLine($"Task {task} stopped!");
                    else
                        Console.WriteLine("Something is wrong!");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void GetStatus()
            {
                Console.Clear();
                Console.WriteLine("Enter task name:");
                var task = Console.ReadLine();
                var result = Proxy.GetTaskStatusRequest(task).Result;
                if (result == "true")
                    Console.WriteLine($"Task {task} is running");
                else
                    Console.WriteLine($"Task {task} is not running");
                try
                {

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void DeleteTask()
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Enter task name:");
                    var task = Console.ReadLine();
                    var result = Proxy.DeleteTaskRequest(task).Result;
                    if (result == "true")
                        Console.WriteLine($"Task {task} deleted");
                    else
                        Console.WriteLine($"Something is wrong");
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void Exit()
            {
                _exitFlag = true;
            }
        }
    }
}