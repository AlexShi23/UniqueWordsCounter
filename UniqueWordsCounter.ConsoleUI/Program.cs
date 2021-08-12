using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace UniqueWordsCounter.ConsoleUI
{
    class Program
    {
        public static async Task Main()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.simbirsoft.com/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var myFormatter = new MyMarkupFormatter();
            var text = document.Body.ToHtml(myFormatter);

            string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' });
            words = words.Where(x => !string.IsNullOrWhiteSpace(x)) // убираем пустые строки
                         .Where(x => x.Any(char.IsLetter)) // убираем строки без букв
                         .ToArray();

            Dictionary<string, uint> res = CountUniqueWords(words);

            foreach (var item in res.OrderByDescending(pair => pair.Value))
                Console.WriteLine($"{item.Key} - {item.Value}");
        }

        public static Dictionary<string, uint> CountUniqueWords(string[] words)
        {
            Dictionary<string, uint> result = new Dictionary<string, uint>();
            foreach(var word in words)
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
