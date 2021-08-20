using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniqueWordsCounter.Core
{
    public class Logger
    {
        public static void LogToFile(string message)
        {
            using (StreamWriter sw = new StreamWriter("ErrorsLog.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString() + " - " + message); // логгирование ошибки в текстовый файл
            }
        }
    }
}
