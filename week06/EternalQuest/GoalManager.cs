using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    private const int PointsPerLevel = 1000;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("========== ETERNAL QUEST ==========");
            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record a goal event");
            Console.WriteLine("3. Display all goals");
            Console.WriteLine("4. Save goals");
            Console.WriteLine("5. Load goals");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;

                case "2":
                    RecordEvent();
                    break;

                case "3":
                    ListGoalDetails();
                    break;

                case "4":
                    SaveGoals();
                    break;

                case "5":
                    LoadGoals();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Thank you for playing Eternal Quest!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose 1-6.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        int level = (_score / PointsPerLevel) + 1;
        int pointsToNextLevel = PointsPerLevel - (_score % PointsPerLevel);

        Console.WriteLine($"Total score: {_score}");
        Console.WriteLine($"Current level: {level}");
        Console.WriteLine($"Points to next level: {pointsToNextLevel}");
    }

    public void ListGoalNames()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("========== YOUR GOALS ==========");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine();
        Console.WriteLine("Choose a goal type:");
        Console.WriteLine("1. Simple goal");
        Console.WriteLine("2. Eternal goal");
        Console.WriteLine("3. Checklist goal");

        int goalType = ReadInt("Goal type: ", 1, 3);

        string shortName = ReadText("Enter a short name: ");
        string description = ReadText("Enter a description: ");
        int points = ReadInt("Enter points awarded: ", 1, 1000000);

        Goal newGoal;

        switch (goalType)
        {
            case 1:
                newGoal = new SimpleGoal(shortName, description, points);
                break;

            case 2:
                newGoal = new EternalGoal(shortName, description, points);
                break;

            default:
                int target = ReadInt(
                    "How many completions are required? ", 1, 1000000);

                int bonus = ReadInt(
                    "Enter the completion bonus: ", 0, 1000000);

                newGoal = new ChecklistGoal(
                    shortName,
                    description,
                    points,
                    target,
                    bonus);

                break;
        }

        _goals.Add(newGoal);

        Console.WriteLine("Goal created successfully.");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("Create a goal before recording an event.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Select a goal to record:");

        ListGoalNames();

        int selection = ReadInt(
            "Enter the goal number: ", 1, _goals.Count);

        Goal selectedGoal = _goals[selection - 1];

        if (selectedGoal.IsComplete())
        {
            Console.WriteLine(
                "This goal is already complete. No additional points awarded.");
            return;
        }

        int pointsEarned = selectedGoal.RecordEvent();

        _score += pointsEarned;

        Console.WriteLine($"Event recorded for: {selectedGoal.GetShortName()}");
        Console.WriteLine($"Points earned: {pointsEarned}");

        if (selectedGoal.IsComplete())
        {
            Console.WriteLine("Congratulations! You completed this goal.");
        }

        Console.WriteLine($"New total score: {_score}");
    }

    public void SaveGoals()
    {
        string filename = ReadText(
            "Enter a filename to save to (for example, goals.txt): ");

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);

                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine("Goals and score saved successfully.");
        }
        catch (IOException)
        {
            Console.WriteLine("Unable to save the file. Check the filename and permissions.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have permission to write to that file.");
        }
    }

    public void LoadGoals()
    {
        string filename = ReadText(
            "Enter the filename to load (for example, goals.txt): ");

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found. No data was loaded.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
            {
                Console.WriteLine("The file is empty.");
                return;
            }

            if (!int.TryParse(lines[0], out int loadedScore) || loadedScore < 0)
            {
                Console.WriteLine("The saved score is invalid.");
                return;
            }

            List<Goal> loadedGoals = new List<Goal>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                try
                {
                    Goal goal = CreateGoalFromString(lines[i]);

                    if (goal != null)
                    {
                        loadedGoals.Add(goal);
                    }
                }
                catch (Exception ex) when (
                    ex is FormatException ||
                    ex is ArgumentException ||
                    ex is IndexOutOfRangeException)
                {
                    Console.WriteLine(
                        $"Skipping invalid goal data on line {i + 1}.");
                }
            }

            _goals = loadedGoals;
            _score = loadedScore;

            Console.WriteLine("Goals and score loaded successfully.");
        }
        catch (IOException)
        {
            Console.WriteLine("Unable to read the saved file.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have permission to read that file.");
        }
    }

    private Goal CreateGoalFromString(string data)
    {
        string[] parts = data.Split('|');

        if (parts.Length < 4)
        {
            throw new FormatException("Incomplete goal data.");
        }

        string type = parts[0];
        string shortName = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);

        if (points <= 0)
        {
            throw new FormatException("Points must be positive.");
        }

        switch (type)
        {
            case "SimpleGoal":
            {
                if (parts.Length != 5)
                {
                    throw new FormatException("Invalid simple goal data.");
                }

                SimpleGoal goal = new SimpleGoal(
                    shortName, description, points);

                bool isComplete = bool.Parse(parts[4]);

                if (isComplete)
                {
                    goal.RecordEvent();
                }

                return goal;
            }

            case "EternalGoal":
            {
                if (parts.Length != 4)
                {
                    throw new FormatException("Invalid eternal goal data.");
                }

                return new EternalGoal(
                    shortName, description, points);
            }

            case "ChecklistGoal":
            {
                if (parts.Length != 7)
                {
                    throw new FormatException("Invalid checklist goal data.");
                }

                int target = int.Parse(parts[4]);
                int bonus = int.Parse(parts[5]);
                int amountCompleted = int.Parse(parts[6]);

                ChecklistGoal goal = new ChecklistGoal(
                    shortName,
                    description,
                    points,
                    target,
                    bonus);

                goal.RestoreProgress(amountCompleted);

                return goal;
            }

            default:
                throw new FormatException("Unknown goal type.");
        }
    }

    private int ReadInt(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int number) &&
                number >= minimum &&
                number <= maximum)
            {
                return number;
            }

            Console.WriteLine(
                $"Enter a whole number between {minimum} and {maximum}.");
        }
    }

    private string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(input) &&
                !input.Contains('|') &&
                !input.Contains('\n') &&
                !input.Contains('\r'))
            {
                return input.Trim();
            }

            Console.WriteLine(
                "Enter a valid value without the | character.");
        }
    }
}