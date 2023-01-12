
using System.Collections.Generic;
using System.IO;

namespace TestTask
{
    internal class WordAnagram
    {
        public readonly Dictionary<int, HashSet<string>> anagrams;
        public WordAnagram()
        {
            anagrams = new Dictionary<int, HashSet<string>>();
        }
        int GetCharsRepresentitiveDecimalsSummation(string word)
        {
            int sum = 0;
            word=word.ToLower();
            foreach (char character in word)
                sum += (int)character;
            return sum;
        }

        public void AddAnagram(string word)
        {
            int sum = GetCharsRepresentitiveDecimalsSummation(word);
            if (anagrams.ContainsKey(sum))
                anagrams[sum].Add(word);
            else
                anagrams.Add(sum, new HashSet<string>() { word });
        }

        public IEnumerable<string> GetAnagaramsOfWord(string word)
        {
            int sum = GetCharsRepresentitiveDecimalsSummation(word);
            if (anagrams.ContainsKey(sum))
              return anagrams[sum].Count>1 ? anagrams[GetCharsRepresentitiveDecimalsSummation(word)] : null;
            return null;
        }

    }
}
