using AngleSharp;
using AngleSharp.Dom;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace UniqueWordsCounter.Core 
{
    public class Parser
    {
        public string Url { get; set; }
        public string[] Separators { get; set; }
        public Parser(string url, string[] separators)
        {
            Url = url;
            Separators = separators;
        }
        public Dictionary<string, uint> GetUniqueWordsList()
        {
            var text = ParseWebPage();
            var words = SplitWords(text.Result);
            return CountUniqueWords(words);
        }
        public async Task<string> ParseWebPage()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            IDocument document;
            try
            {
                document = await context.OpenAsync(this.Url);
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message);
                throw ex;
            }

            var myFormatter = new MyMarkupFormatter();
            string text;
            try
            {
                text = document.Body.ToHtml(myFormatter);
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message);
                throw ex;
            }
            return text;
        }

        public string[] SplitWords(string text)
        {
            Regex trimmer = new Regex(@"\s\s+"); // регулярное выражение, чтобы убрать несколько пробелов
            string[] words = text.Split(this.Separators, StringSplitOptions.None);
            return words.Where(x => !string.IsNullOrWhiteSpace(x)) // убираем пустые строки
                         .Where(x => x.Any(char.IsLetter)) // убираем строки без букв
                         .Select(x => x.Replace("\n", "")) // убираем переносы строки
                         .Select(x => trimmer.Replace(x, "")) // убираем лишние пробелы
                         .ToArray();
        }

        public Dictionary<string, uint> CountUniqueWords(string[] words) 
        {
            Dictionary<string, uint> result = new Dictionary<string, uint>();
            foreach (var word in words)
            {
                if (result.ContainsKey(word))
                    result[word]++;
                else
                    result.Add(word, 1);
            }
            return result;
        }
    }
}