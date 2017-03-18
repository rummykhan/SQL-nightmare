using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_nightmare
{
    class Program
    {
        static void Main(string[] args)
        {
            QueriesDB.initialize();

            UserInteraction.showBanner();

            string basicURL = null;

            while (basicURL != "x")
            {
                basicURL = null;
                Log.logNotification("Press 'x' for exit..");

                UserInteraction.promptForUserInput("Enter url");
                basicURL = Console.ReadLine();

                Exploitation.initialize(basicURL);


                if (basicURL != "x" && !String.IsNullOrEmpty(basicURL))
                {
                    int Choice = -1;

                    while (Choice != 0)
                    {
                        Choice = UserInteraction.showMenuForIntChoice("Press '0' for Exit..\nPress '1' for directory navigation..\nPress '2' for dumping data.." +
                        "\nPress '3' for files reading..\nPress '4' for shell uploading..");

                        if (Choice == 1)
                            DirListing.initialize(basicURL);
                        else if (Choice == 2)
                            DumpData.initialize(basicURL);
                        else if (Choice == 3)
                            ReadFile.Read(basicURL);
                        else if (Choice == 4)
                            ShellSpawning.Spawn(basicURL);
                    }
                }
            }

            Log.logOutput("Program is going to exit.. Press any key..");
            Console.ReadKey();
        }
    }
}
