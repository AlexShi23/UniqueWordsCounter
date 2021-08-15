using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                using (StreamWriter sw = new StreamWriter("ErrorsLog.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " - " + ex.Message); // логгирование ошибки в текстовый файл
                }
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
                using (StreamWriter sw = new StreamWriter("ErrorsLog.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " - " + ex.Message); // логгирование ошибки в текстовый файл
                }
                throw ex;
            }
            return text;
        }

        public string[] SplitWords(string text)
        {
            string[] words = text.Split(this.Separators, StringSplitOptions.None);
            return words.Where(x => !string.IsNullOrWhiteSpace(x)) // убираем пустые строки
                         .Where(x => x.Any(char.IsLetter)) // убираем строки без букв
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