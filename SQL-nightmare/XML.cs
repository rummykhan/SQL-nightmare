using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SQL_nightmare
{
    class XML
    {
        public static string addRoot(string response)
        {
            response = "<xoxo>" + response + "</xoxo>";
            return response;
        }

        public static void parseXML(string tableName, string fileNameToWrite, string[] columnsToDisplay)
        {
            int counter = 1;
            XmlDocument xDoc = new XmlDocument();

            xDoc.Load("tmp.txt");

            XmlNodeList nodeList = xDoc.GetElementsByTagName(tableName);

            using (StreamWriter sw = new StreamWriter(fileNameToWrite))
            {
                sw.WriteLine("Table : " + tableName);
                sw.Flush();
                Log.logOutput("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.Flush();

                string columns = "|";
                foreach (var item in columnsToDisplay)
                {
                    columns += "\t" + item + "\t|";
                }

                Log.logOutput(columns);

                sw.WriteLine(columns);
                sw.Flush();

                Log.logOutput("---------------------------------------------------------------------------");
                sw.Flush();

                foreach (XmlNode node in nodeList)
                {
                    string oneRecord = "|";
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        oneRecord += "\t" + attribute.Value + "\t| ";
                    }
                    Log.logOutput(oneRecord);

                    sw.WriteLine(oneRecord);
                    sw.Flush();
                    Log.logOutput("---------------------------------------------------------------------------");
                    sw.Flush();
                    counter++;
                }
            }
        }

        public static List<string> parseXML(string xmlFileName, string tagName)
        {
            try
            {
                List<string> dirInfo = new List<string>();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(xmlFileName);
                XmlNodeList nodeList = xDoc.GetElementsByTagName(tagName);
                foreach (XmlNode node in nodeList)
                {
                    dirInfo.Add(node.Attributes[0].Value);
                }
                return dirInfo;
            }
            catch (Exception ex)
            {
                Log.logError(ex.Message);
                return null;
            }
        }

    }
}
