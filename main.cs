using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

class Program
{
    private MyDictionary dictionary;
    private string[] scrabbleLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private int[] scrabblePoints = { 1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 5, 1, 3, 4, 1, 3, 10, 1, 1, 1, 1, 4, 4, 8, 4, 10 };
    private string[] playerWords = new string[100];
    private int playerPoints;
    private int playerCount;
    private Random rnd = new Random();
    private string[] randomLetters = new string[7];
    private HashSet<string> checkedWords = new HashSet<string>();

    public Program()
    {
        dictionary = new MyDictionary();
    }
    public void Run()
    {
        Console.WriteLine("Welcome to Scrabble!");

        // Generate random letters for the player
        while (true)
        {
            // Generate random letters
            for (int j = 0; j < 7; j++)
            {
                randomLetters[j] = scrabbleLetters[rnd.Next(scrabbleLetters.Length)];
            }

            // Check if there is a word that can be formed with the random letters
            bool canFormWord = false;
            string[] vowels = { "a", "e", "i", "o", "u" };
            //use a for loop to loop through the array vowels to check if randomLetters contains at least one vowel

            for (int j = 0; j < 7; j++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (randomLetters[j].Contains(vowels[x]))
                    {
                        break;
                        Console.WriteLine("has vowels");
                    }
                    else
                    {
                        Console.WriteLine("no vowels");

                        break;

                    }


                }
                //if (checkIfWordCanBeFormed(randomLetters[j]))
                //{
                //    canFormWord = true;
                //    break;
                //}
            }

            // If a word can be formed, display the letters to the user
            if (canFormWord)
            {
                Console.WriteLine("Your random letters are: ");
                for (int j = 0; j < 7; j++)
                {
                    string letters = randomLetters[j];
                    Console.Write(letters.ToUpper());
                }

                break;
            }
        }

        // Game logic for the player
        Console.WriteLine("\nEnter a word or type '!exit' to quit: ");
        while (true)
        {
            string word = Console.ReadLine();

            if (word == "!exit")
            {
                break;
            }

            if (dictionary.CheckWord(word) == false)
            {
                Console.WriteLine("Invalid word. Please try again.");
                continue;
            }

            if (!checkIfWordCanBeFormed(word))
            {
                Console.WriteLine("This word cannot be formed with the given letters. Please try again.");
                continue;
            }

            playerWords[playerCount] = word;
            playerPoints += CalculatePoints(word);
            playerCount++;

            Console.WriteLine("Word added! Current score: " + playerPoints);
        }

        // Display the final score
        Console.WriteLine("\nYour final score is " + playerPoints);
    }
    private int CalculatePoints(string word)
    {
        string tempWord = word;
        word = word.ToLower();
        int points = 0;
        for (int i = 0; i < word.Length; i++)
        {
            for (int j = 0; j < scrabbleLetters.Length; j++)
            {
                if (word[i].Equals(scrabbleLetters[j]))
                {
                    points += scrabblePoints[j];
                    break;
                }
            }
        }
        return points;
        word = tempWord;
    }

    private bool checkIfWordCanBeFormed(string word, int shuffleLimit = 10)
    {
        if (checkedWords.Contains(word) || shuffleLimit < 0)
        {
            return false;
        }

        string[] wordLetters = word.ToLower().ToCharArray().Select(c => c.ToString()).ToArray();
        string[] randomLettersCopy = (string[])randomLetters.Clone();
        bool canBeFormed = true;
        foreach (string letter in wordLetters)
        {
            if (!randomLettersCopy.Contains(letter))
            {
                canBeFormed = false;
                break;
            }
            else
            {
                randomLettersCopy = randomLettersCopy.Where(val => val != letter).ToArray();
            }
        }

        if (!canBeFormed)
        {
            // check if shuffling the letters can form a word
            var shuffled = word.OrderBy(c => rnd.Next()).ToArray();
            var shuffledWord = new string(shuffled);
            if (checkIfWordCanBeFormed(shuffledWord, shuffleLimit - 1))
            {
                return true;
            }
            checkedWords.Add(word);
            return false;
        }
        else
        {
            return true;
        }
    }

    private void shuffleLetters()
    {
        // File path of the original text file
        string originalFilePath = @"C:\Users\benwt\Documents\College\Computer Science\VS Projects\test\test\wordsData.txt";

        // File path of the new text file to save the scrambled lines
        string newFilePath = @"C:\Users\benwt\Documents\College\Computer Science\VS Projects\test\test\wordsShuffledData.txt";

        // Read each line of the original text file
        string[] lines = File.ReadAllLines(originalFilePath);

        // Scramble the letters of each line
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new string(lines[i].ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        // Write the scrambled lines to the new text file
        File.WriteAllLines(newFilePath, lines);
    }
}
