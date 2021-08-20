using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniqueWordsCounter.Core;

namespace UniqueWordsCounter.WebUI.Models
{
    public class ParserViewModel
    {
        public string Url { get; set; }
        public string Separators { get; set; }
        public Parser parser;
        public Dictionary<string, uint> Result { get; set; }
        private string resultString;
        public string ResultString
        {
            get
            {
                if (resultString == null)
                {
                    resultString = Count();
                }
                return resultString;
            }
            set
            {
                resultString = value;
            }
        }
        public string Error { get; set; }
        public ParserViewModel()
        {

        }
        public ParserViewModel(string url, string separators)
        {
            Url = url;
            Separators = separators;
        }

        private string Count()
        {
            string resultString = "";
            if (Validate())
            {
                string[] separators = Separators.Split(", ");
                for (int i = 0; i < separators.Length; i++)
                    separators[i] = separators[i].Trim('\'');
                    
                parser = new Parser(Url, separators);

                try
                {
                    Result = parser.GetUniqueWordsList();
                }
                catch(Exception ex)
                {
                    Error = ex.Message;
                    return "";
                }

                foreach (var item in Result.OrderByDescending(pair => pair.Value))
                    resultString += $"{item.Key} - {item.Value}\n";
            }
            return resultString;
        }

        public bool Validate()
        {
            if (Error != null)
                return false;
            if (string.IsNullOrEmpty(Url))
            {
                Error = "Вы ввели пустой адрес сайта!";
                Logger.LogToFile(Error);
                return false;
            }
            else if (string.IsNullOrEmpty(Separators))
            {
                Error = "Вы ввели пустой список разделителей!";
                Logger.LogToFile(Error);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
