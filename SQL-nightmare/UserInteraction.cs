using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class UserInteraction
    {
        public static void showBanner()
        {
            Console.Title = "rummykhan's SQLi";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t\t: Love For :\n | Lafangoo | Ch3rn0by1 | Connecting | exploiter-z | Gujjar (PCP) | rootxx |\n\t |PMH's Str!k3r -" +
                              "Rafay Baloch -Jin -hussein(h98d) -Zen -Rahul| \n\t\t|MakMan--madCodE--Blackhawk--Ajkaro--benzi| ");
            Console.WriteLine();
        }

        public static string takeInputForTableFileGeneration()
        {
            promptForUserInput("Enter Name for File");
            string fileName = Console.ReadLine();

            fileName += (new Random().Next(123456789)).ToString();
            return fileName + ".txt";
        }

        public static void promptForUserInput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(message + " :: ");
        }
        
        public static int showMenuForIntChoice(string message)
        {
            promptForUserInput(message);

            string userInputString = Console.ReadLine();
            int userInputInt = 0;

            if (int.TryParse(userInputString, out userInputInt))
                return userInputInt;
            else
            {
                Log.logError("Bad input");
                return -1;
            }

        }

        public static string takeInputString(string message)
        {
            promptForUserInput(message);
            return Console.ReadLine();
        }
    }
}
