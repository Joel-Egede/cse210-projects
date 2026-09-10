public class PromptGenerator
{
    public List<string> _prompts = new List<string>
    { 
        "What did you learn today?",
        "What was the best part of your day?",
        "What are you grateful for?",
        "What challenges did you face today?",
        "What is something you want to improve?"  

    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}


