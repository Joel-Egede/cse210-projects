public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(
        string shortName,
        string description,
        int points,
        int target,
        int bonus)
        : base(shortName, description, points)
    {
        if (target <= 0)
        {
            throw new System.ArgumentException(
                "The target must be greater than zero.");
        }

        if (bonus < 0)
        {
            throw new System.ArgumentException(
                "The bonus cannot be negative.");
        }

        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountCompleted++;

        int pointsEarned = GetPoints();

        if(_amountCompleted == _target)
        {
            pointsEarned += _bonus;

            Console.WriteLine($"Congratulations! You earned a bonus of {_bonus} points for completing the goal!");
        }

        return pointsEarned;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";

        return $"{checkbox} {GetShortName()} ({GetDescription()}) " +
               $"-- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{GetShortName()}|{GetDescription()}|" +
               $"{GetPoints()}|{_target}|{_bonus}|{_amountCompleted}";
    }

    public int GetAmountCompleted()
    {
        return _amountCompleted;
    }

    public int GetTarget()
    {
        return _target;
    }

    public int GetBonus()
    {
        return _bonus;
    }

    public void RestoreProgress(int amountCompleted)
    {
        if (amountCompleted < 0 || amountCompleted > _target)
        {
            throw new System.ArgumentException(
                "Saved progress is outside the valid range.");
        }

        _amountCompleted = amountCompleted;
    }
}