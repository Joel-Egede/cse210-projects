public class Comment
{
    // Stores the name of the person who made the comment.
    private string _name;

    // Stores the text of the comment.
    private string _text;

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetText()
    {
        return _text;
    }
}