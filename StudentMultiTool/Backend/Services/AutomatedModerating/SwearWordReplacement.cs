using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automated_Moderating_demo
{
    public sealed class SwearWordReplacement
    {
        public SwearWordReplacement(IEnumerable<KeyValuePair<string, string>> replacements)
        {
            Replacements = replacements.Select(p => new ProfanityFilter(p.Key, p.Value)).ToList().AsReadOnly();
        }

        public IReadOnlyList<ProfanityFilter> Replacements { get; }

        public string Clbuttify(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                foreach (var replacement in Replacements)
                {
                    message = replacement.Replace(message);
                }
            }

            return message;
        }
    }
}
