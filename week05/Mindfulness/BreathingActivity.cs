using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through slow breathing. " +
            "Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < Duration)
        {
            Console.Write("Breathe in... ");
            int inSeconds = Math.Min(4, GetRemainingSeconds(startTime));
            if (inSeconds <= 0) break;
            ShowCountDown(inSeconds);
            Console.WriteLine();

            if ((DateTime.Now - startTime).TotalSeconds >= Duration)
                break;

            Console.Write("Breathe out... ");
            int outSeconds = Math.Min(4, GetRemainingSeconds(startTime));
            if (outSeconds <= 0) break;
            ShowCountDown(outSeconds);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}
