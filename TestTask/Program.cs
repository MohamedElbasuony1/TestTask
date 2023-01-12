using System;
using System.Collections.Generic;
using System.IO;

namespace TestTask
{
    internal class Program
    {
        static string[] LoadInputsFromFile(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            return reader.ReadToEnd().Split(' ');
        }
        static void PrintAnagramsToFile(string filePath, Dictionary<int, HashSet<string>> anagrams)
        {
            string output = "";
            foreach (var item in anagrams)
                if (item.Value.Count > 1)
                    output += $"[{string.Join(',', item.Value)}]\n";
            File.WriteAllText(filePath, output);
        }

        static int GetTheOriginalNumberOfFactorial(decimal factorial)
        {
            for (int num = 0; ; num++)
            {
                if (factorial % (num + 1) == 0)
                    factorial /= num + 1;
                else if (factorial == 1)
                    return num;
                else
                    return 0; // it is not a factorial of any number;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("** Please read the instructions below **\n");
            Console.WriteLine("1- You will find a text file with name words with some initial data at the root folder of the application.");
            Console.WriteLine("2- You should fill it with numbers or words seperated with space in order to calculate your results.");
            Console.WriteLine("3- You should see all the anagrams compinations found in the words file in another text file called anagrams in the folder.");
            Console.WriteLine("4- If you changed the words file you should restat the application in order to see the updates.\n");
            Console.WriteLine("* For example try to type **cat** to get all its anagrams or type **720** to see whether that 720 is the factorial of any numbers in the words file.\n");

            string[] inputs = LoadInputsFromFile("../../../words.txt");
            List<int> numbers = new List<int>();
            WordAnagram wordAnagram = new WordAnagram();           
            foreach (var item in inputs)
            {
                if(!int.TryParse(item,out int number))
                    wordAnagram.AddAnagram(item);
                else
                    numbers.Add(number);
            }

            PrintAnagramsToFile("../../../anagrams.txt",wordAnagram.anagrams);

            while (true)
            {
                Console.WriteLine("Please enter a word to get its anagrams found in words.txt file\nOr you can enter a number to see if it is a factorial of any numbers found in words.txt file.\n");
                string input = Console.ReadLine();
                Console.Write("* output => ");
                if (decimal.TryParse(input, out decimal factorial))
                {
                    int number = GetTheOriginalNumberOfFactorial(factorial);
                    if (number != 0)
                    {
                        if (numbers.Contains(number))
                            Console.WriteLine($"Yes the number is a factorial of one of the numbers existed in words.file");
                        else
                            Console.WriteLine($"the number is not a factorial of any numbers existed in words.file");
                    }    
                    else
                        Console.WriteLine($"the factorial which you provide is not valid");
                }
                else
                {
                    IEnumerable<string> anagrams=wordAnagram.GetAnagaramsOfWord(input);
                    if (anagrams != null)
                        Console.WriteLine($"the anagarms are : [{string.Join(',',anagrams)}]");
                    else
                        Console.WriteLine($"There are no anagarams found in the words.txt file for your word : {input}");
                }

                Console.WriteLine();

            }
        }

    }
}
