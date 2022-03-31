using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentMultiTool.Backend.Services.AutomatedModerating
{
    public struct SwearWord
    {
        public SwearWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) throw new ArgumentNullException(nameof(word));

            int startIndex = 0;
            int length = word.Length;

            while (length > 0 && char.IsWhiteSpace(word[startIndex]))
            {
                startIndex++;
                length--;
            }
            while (length > 0 && char.IsWhiteSpace(word[startIndex + length - 1]))
            {
                length--;
            }

            if (length > 0 && word[startIndex + length - 1] == '!')
            {
                CaseSensitive = true;
                length--;
            }
            else
            {
                CaseSensitive = false;
            }

            if (length > 0 && word[startIndex + length - 1] == '*')
            {
                Suffix = "(?=\\w*\\b)";
                length--;
            }
            else
            {
                Suffix = "\\b";
            }

            if (length > 0 && word[startIndex] == '*')
            {
                Prefix = "(?,=\\b\\w)";
                startIndex++;
                length--;
            }
            else
            {
                Prefix = "\\b";
            }

            Word = length != 0 ? word.Substring(startIndex, length) : null;
        }

        public string Word { get; }
        public string Prefix { get; }
        public string Suffix { get; }
        public bool CaseSensitive { get; }

        public Regex ToRegularExpression()
        {
            if (string.IsNullOrWhiteSpace(Word)) return null;

            string pattern = Prefix + Regex.Escape(Word) + Suffix;
            var options = CaseSensitive ? RegexOptions.ExplicitCapture :
                RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase;
            return new Regex(pattern, options);
        }

    }

}
