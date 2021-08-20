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
            Console.Write("Введите адрес сайта (с указанием протокола): ");
            string address = Console.ReadLine();
            while (!Uri.IsWellFormedUriString(address, UriKind.Absolute))
            {
                Console.Write("Введён некорректный адрес сайта, попробуйте снова: ");
                address = Console.ReadLine();
            }

            Console.Write("Введите список разделителей (для завершения введите end): ");
            List<string> separators = new List<string>();
            var separator = Console.ReadLine();
            while (separator != "end" || separators.Count == 0)
            {
                separators.Add(separator);
                separator = Console.ReadLine();
            }

            Parser parser = new Parser(address, separators.ToArray());
            Dictionary<string, uint> result = parser.GetUniqueWordsList();

            foreach (var item in result.OrderByDescending(pair => pair.Value))
                Console.WriteLine($"{item.Key} - {item.Value}");

            Console.WriteLine("Хотите сохранить результат в базу данных? (да/нет)");
            string answer = Console.ReadLine().ToLower();
            while(answer != "да" && answer != "нет")
            {
                Console.WriteLine("Некорректный ответ, попробуйте снова: ");
                answer = Console.ReadLine().ToLower();
            }
            if (answer == "да")
            {
                DbSaver.SaveToDatabase(result, address);
                Console.WriteLine("Результат сохранён в базу данных");
            }
        }
    }
}
