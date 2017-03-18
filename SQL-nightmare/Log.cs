using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class Log
    {
        public static void logError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + message + " [ERROR]");
        }

        public static void logOutput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }

        public static void logNotification(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[INFO] " + message);
        }

        public static void showObjects(string[] objects, string title)
        {
            try
            {
                logOutput("--[ " + title + "(s) START ]--");
                for (int i = 0; i < objects.Length; i++)
                {
                    logOutput("[" + (i + 1) + "] = " + objects[i]);
                }
                logOutput("--[ " + title + "(s) END ]--");
            }
            catch (NullReferenceException ex)
            {
                logError(ex.Message);
            }
        }

        public static void showObjects(List<string> objects, string title)
        {
            try
            {
                logOutput("--[ " + title + " START ]--");
                for (int i = 0; i < objects.Count; i++)
                {
                    logOutput("[" + (i + 1) + "] = " + objects[i]);
                }
                logOutput("--[ " + title + " END ]--");
            }
            catch (NullReferenceException ex)
            {
                logError(ex.Message);
            }
        }

    }
}
