using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class QueryCrafter
    {
        public static string constructURLForOutputFromTempTable(string url)
        {
            url = url.Replace("'rummykhan'", "oxp");
            url = url.Replace("--X-", " FROM xrummyTables--X-");
            return url;
        }

        public static string constructURLForDroppingObject(string url, string dropTableStackedQuery)
        {
            return url.Replace("--X-", dropTableStackedQuery);
            
        }

        public static string constructURLForConfirmation(string url, string _replacement)
        {
            return url.Replace("rummykhan", _replacement);
        }

        public static string construcQueryForTableDump(string url, string stackedQuery, string _replacement)
        {
            string newUrl = url.Replace("rummykhan", _replacement);

            return newUrl.Replace("--X-", stackedQuery);
        }

        public static string constructQueryForColumns(string columnStackedQuery, string tableName)
        {
            return columnStackedQuery.Replace("[TABLENAME]", "'" + tableName + "'");
        }

        public static string constructQueryForDataDump(string url, string tableName, string tableDumpQuery)
        {
            url = url.Replace("'rummykhan'", tableDumpQuery);
            url = url.Replace("TABLENAME", tableName);
            return url;
        }

        public static string constructStackedQuery(string url, string queryToAttachAtEnd)
        {
            return url.Replace("--X-", queryToAttachAtEnd);
        }

        public static string constructQueryForSelectObject(string url, string objectVerifyingQuery)
        {
            return url.Replace("'rummykhan'", objectVerifyingQuery);
        }
    }
}
