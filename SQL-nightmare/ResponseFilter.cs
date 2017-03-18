using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_nightmare
{
    class ResponseFilter
    {
        public static string[] parseResponce(string response)
        {
            try
            {
                if (response != null)
                {
                    return response.Split(':');
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Log.logError(ex.Message);
                return null;

            }
        }

        public static string getPureResponse(string response)
        {
            try
            {

                int start = response.IndexOf("x102x:");
                int end = response.IndexOf(":x102y");
                int contentLength = end - start;
                response = response.Substring(start, contentLength);
                return response.Replace("x102x:", "");
            }
            catch (Exception ex)
            {
                Log.logError(" while parsing response : " + ex.Message);
                return null;
            }
        }

        public static string getPureResponseWithLastIndex(string response)
        {
            try
            {

                int start = response.LastIndexOf("x102x:");
                int end = response.LastIndexOf(":x102y");
                int contentLength = end - start;
                response = response.Substring(start, contentLength);
                return response.Replace("x102x:", "");
            }
            catch (Exception ex)
            {
                Log.logError(" while parsing response : " + ex.Message);
                return null;
            }
        }

        public static bool confirmResponce(string url, string _replacement)
        {
            try
            {
                if (HTTPMethods.getResponse(url).Contains(_replacement))
                    return true;
                else
                    return false;
            }
            catch (NullReferenceException ex)
            {
                Log.logError("No Responce returned from the Server");
                return false;
            }
        }
    }
}
