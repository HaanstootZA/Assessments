using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Assessment.Ozow.Sorting
{
    public class SimpleSorter : IStringSorter
    {
        public string SortString(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            int[] characterCounter = this.BuildCharacterCounter(input);
            return this.TranslateCharacterCounter(characterCounter);
        }

        private int[] BuildCharacterCounter(string input)
        {
            int[] characterCounter = new int[26];
            foreach (char c in input)
            {
                int index = c < 0x61 ? c - 0x41 : c - 0x61;
                if (index < 0 || index > 26)
                {
                    continue;
                }

                characterCounter[index] += 1;
            }
            return characterCounter;
        }

        private string TranslateCharacterCounter(int[] characterCounter)
        {
            StringBuilder resultBuilder = new StringBuilder();
            for (int i = 0; i < characterCounter.Length; i++)
            {
                while (characterCounter[i] > 0)
                {
                    resultBuilder.Append((char)(i + 0x61));
                    characterCounter[i] -= 1;
                }
            }

            return resultBuilder.ToString();
        }
    }
}
