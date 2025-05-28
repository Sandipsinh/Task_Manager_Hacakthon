using Azure.AI.OpenAI;

public class ChatResponse
{
    public List<choices> Choices { get; set; }
}

public class choices
{
    public message Message { get; set; }
}

public class message
{
    public string Content { get; set; }
}