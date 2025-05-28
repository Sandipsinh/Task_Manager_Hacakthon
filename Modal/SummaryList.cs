public class SummaryList
{
    public List<SummaryRequest> Summaries { get; set; }
}
public class SummaryRequest
{
    public string Role { get; set; }
    public string Content { get; set; }
}