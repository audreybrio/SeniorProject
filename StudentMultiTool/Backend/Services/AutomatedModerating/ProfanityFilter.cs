using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Automated_Moderating_demo
{
    public sealed class ProfanityFilter
    {
        public ProfanityFilter(SwearWord word, string replacement)
        {
            if (string.IsNullOrWhiteSpace(word.Word)) throw new ArgumentNullException(nameof(word));

            Pattern = word.ToRegularExpression();
            CaseSensitive = word.CaseSensitive;
            Replacement = replacement;

            if (CaseSensitive || replacement == null || replacement.Any(char.IsUpper))
            {
                Replacer = (Match m) => Replacement;
            }
            else
            {
                Replacer = (Match m) => MatchCase(m.Value, Replacement);
            }
        }

        public ProfanityFilter(string word, string replacement) : this(new SwearWord(word), replacement)
        {

        }

        public Regex Pattern { get; }
        public string Replacement { get; }
        public bool CaseSensitive { get; }
        public MatchEvaluator Replacer { get; }

        public static string MatchCase(string wordToReplace, string replacement)
        {
            if (null == replacement) return string.Empty;
            if (wordToReplace.All(char.IsLower)) return replacement;
            if (wordToReplace.All(char.IsUpper)) return replacement.ToUpperInvariant();

            char[] result = replacement.ToCharArray();
            bool changed = false;

            if (wordToReplace.Length == replacement.Length)
            {
                for (int index = 0; index < result.Length; index++)
                {
                    if (char.IsUpper(wordToReplace[index]))
                    {
                        char c = result[index];
                        result[index] = char.ToUpperInvariant(c);
                        if (result[index] != c) changed = true;
                    }
                }
            }
            else
            {
                if (char.IsUpper(wordToReplace[0]))
                {
                    char c = result[0];
                    result[0] = char.ToLowerInvariant(c);
                    if (result[0] != c) changed = true;
                }
                if (char.IsUpper(wordToReplace[wordToReplace.Length - 1]))
                {
                    int index = result.Length - 1;
                    char c = result[index];
                    result[index] = char.ToUpperInvariant(c);
                    if (result[index] != c) changed = true;
                }
            }

            return changed ? new string(result) : replacement;

        }

        public string Replace(string input) => Pattern.Replace(input, Replacer);

    }
}
