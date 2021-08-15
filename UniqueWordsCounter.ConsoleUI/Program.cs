using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using UniqueWordsCounter.Core;

namespace UniqueWordsCounter.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Введите адрес сайта: ");
            string address = Console.ReadLine();
            while (!Uri.IsWellFormedUriString(address, UriKind.Absolute))
            {
                Console.Write("Введён некорректный адрес сайта, попробуйте снова: ");
                address = Console.ReadLine();
            }

            Console.WriteLine("Введите список разделителей: (для завершения введите end)");
            List<string> separators = new List<string>();
            var separator = Console.ReadLine();
            while (separator != "end")
            {
                separators.Add(separator);
                separator = Console.ReadLine();
            }

            Parser parser = new Parser(address, separators.ToArray());
            Dictionary<string, uint> result = parser.GetUniqueWordsList();

            foreach (var item in result.OrderByDescending(pair => pair.Value))
                Console.WriteLine($"{item.Key} - {item.Value}");
        }
    }
}
