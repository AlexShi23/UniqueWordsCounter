using AngleSharp;
using AngleSharp.Dom;
using System;

namespace UniqueWordsCounter.Core
{
    public class MyMarkupFormatter : IMarkupFormatter
    {
        String IMarkupFormatter.Comment(IComment comment)
        {
            return String.Empty;
        }

        String IMarkupFormatter.Doctype(IDocumentType doctype)
        {
            return String.Empty;
        }

        String IMarkupFormatter.Processing(IProcessingInstruction processing)
        {
            return String.Empty;
        }

        String IMarkupFormatter.Text(ICharacterData text)
        {
            return text.Data;
        }

        String IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing)
        {
            switch (element.LocalName)
            {
                case "p":
                    return "\n\n";
                case "br":
                    return "\n";
                case "span":
                    return " ";
                case "div":
                    return " ";
                case "a":
                    return " ";
            }

            return String.Empty;
        }

        String IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing)
        {
            return String.Empty;
        }

        public string LiteralText(ICharacterData text)
        {
            return String.Empty;
        }
    }
}