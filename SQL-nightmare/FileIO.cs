using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SQL_nightmare
{
    class FileIO
    {
        public static void deleteTempFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        public static bool createWriteFile(string contents, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(contents);
                    sw.Flush();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.logError(ex.Message);
                return false;
            }
        }
    }
}
