using System;

public class Program
{
    // Creativity requirement:
    // This project includes a simple session statistic. After each activity,
    // the program records how many activities have been completed and displays
    // the total when the user exits.
    public static void Main(string[] args)
    {
        int completedActivities = 0;
        bool running = true;
        

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.WriteLine();
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    completedActivities++;
                    PauseBeforeMenu();
                    break;

                case "2":
                    ReflectingActivity reflecting = new ReflectingActivity();
                    reflecting.Run();
                    completedActivities++;
                    PauseBeforeMenu();
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    completedActivities++;
                    PauseBeforeMenu();
                    break;

                case "4":
                    Console.WriteLine();
                    Console.WriteLine($"You completed {completedActivities} mindfulness activity(ies).");
                    Console.WriteLine("Thank you for using the Mindfulness Program.");
                    running = false;
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice. Please select 1, 2, 3, or 4.");
                    Thread.Sleep(1500);
                    break;
            }
        }
    }

    private static void PauseBeforeMenu()
    {
        Console.WriteLine("Returning to the menu...");
        Thread.Sleep(2000);
    }
}
