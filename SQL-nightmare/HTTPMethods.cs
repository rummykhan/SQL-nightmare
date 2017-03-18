using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace SQL_nightmare
{
    class HTTPMethods
    {
        public static string getResponse(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 500000;

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
                    string contents = reader.ReadToEnd();
                    return HttpUtility.HtmlDecode(contents);
                }
            }
            catch (WebException wc)
            {
                try
                {
                    WebResponse wr = (WebResponse)wc.Response;
                    using (var stream = wr.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string contents = reader.ReadToEnd();
                        return HttpUtility.HtmlDecode(contents);
                    }
                }
                catch (NullReferenceException ex)
                {
                    Log.logError("Plz check you internet connection OR website has blocked you ip - TimeOUT");
                    return null;
                }
            }
            catch (UriFormatException ex)
            {
                Log.logError(ex.Message);
                return null;
            }
            catch (NullReferenceException ex)
            {
                Log.logError(ex.Message);
                return null;
            }
        }

    }
}
