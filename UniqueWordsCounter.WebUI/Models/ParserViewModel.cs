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
        private string result;
        public string Result
        {
            get
            {
                if (result == null)
                {
                    result = Count();
                }
                return result;
            }
            set
            {
                result = value;
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
            string result = "";
            if (Validate())
            {
                string[] separators = Separators.Split(", ");
                for (int i = 0; i < separators.Length; i++)
                    separators[i] = separators[i].Trim('\'');

                Parser parser = new Parser(Url, separators);

                Dictionary<string, uint> dict = parser.GetUniqueWordsList();

                foreach (var item in dict.OrderByDescending(pair => pair.Value))
                    result += $"{item.Key} - {item.Value}\n";
            }
            return result;
        }

        public bool Validate()
        {
            if (Error != null)
                return false;
            if (string.IsNullOrEmpty(Url))
            {
                Error = "Вы ввели пустой адрес сайта!";
                return false;
            }
            else if (string.IsNullOrEmpty(Separators))
            {
                Error = "Вы ввели пустой список разделителей!";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
