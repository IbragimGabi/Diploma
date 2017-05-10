using System;
using System.Collections.Generic;

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

            public Menu()
            {
                InitMenu();
            }

            private void InitMenu()
            {
                _menu = new Dictionary<int, string>();
                _menu.Add(1, "Create task");
                _menu.Add(2, "Start task");
                _menu.Add(3, "Stop task");
                _menu.Add(4, "Get task's status");
                _menu.Add(5, "Get task's results");
                _menu.Add(6, "Delete task");
                _menu.Add(7, "Exit");
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
                Console.WriteLine("Choose row and enter row number:");

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
                        return GetResults;
                    case 6:
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

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void GetStatus()
            {
                Console.Clear();
                Console.WriteLine("Please enter username for close his sessions:");

                try
                {

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }

            private void GetResults()
            {
                Console.Clear();
                Console.WriteLine("Trying to close all sessions...");

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

            private delegate void CallDelegate();

        }
    }
}