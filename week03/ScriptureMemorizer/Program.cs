using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Creativity feature:
        // Instead of using only one scripture, this program contains
        // a small library of scriptures and randomly selects one
        // each time the program starts.

        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son that whoever believes in him should not perish but have eternal life."),

            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all your heart and lean not on your own understanding in all your ways acknowledge him and he will make your paths straight."),

            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ who strengthens me.")
        };

        Random random = new Random();
        Scripture scripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");

            string input = Console.ReadLine() ?? "";

            if (input.Trim().ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);

            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                break;
            }
        }
    }
}