using System;

public class Program
{
    public static void Main(string[] args)
    {
        /*
         * Creativity and exceeding requirements:
         *
         * This program includes a leveling system. Users gain levels
         * as they accumulate points, giving them an additional reward
         * for making progress toward their goals.
         *
         * The program also validates user input, prevents repeated
         * rewards for completed goals, and restores goal progress
         * when saved data is loaded.
         */

        GoalManager manager = new GoalManager();

        manager.Start();
    }
}