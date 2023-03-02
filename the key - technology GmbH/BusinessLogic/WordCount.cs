using System.Text.RegularExpressions;

namespace the_key___technology_GmbH.BusinessLogic
{
    public class WordCount
    {
        public static IDictionary<string, int> GetWordCount(string content)
        {
            IDictionary<string, int> wordCount = new Dictionary<string, int>();
            content = Regex.Replace(content, "<.*?>", string.Empty);        // Not sure this is a good way to strip HTML, but good enough for a coding homework.
            content = Regex.Replace(content, "[\"',.;:()]", string.Empty);

            string[] allWords = content.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (string word in allWords)
            {
                if (wordCount.Keys.Contains(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount.Add(word, 1);
                }
            }

            return wordCount;
        }
    }
}
