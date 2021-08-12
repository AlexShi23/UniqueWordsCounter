using System;
using System.Collections.Generic;
using System.Text;

namespace UniqueWordsCounter.Core.Models
{
    public class Parser : IParser
    {
        public string Url { get; set; }
        public string[] Separators { get; set; }
        public Parser(string url, string[] separators)
        {
            Url = url;
            Separators = separators;
        }

        public string GetAllText()
        {
            return "";
        }
        public string[] SplitWords()
        {
            return new string[1];
        }
        public Dictionary<string, uint> CountUniqueWords(string[] words)
        {
            return new Dictionary<string, uint>();
        }
    }
}
