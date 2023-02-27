namespace GeoPet.Validation.Base;

public class Report
{
    public string? Code { get; set; }
    public string? Message { get; set; }

    public Report()
    {

    }

    public Report(string message)
    {
        Message = message;
    }

    public static Report Create(string message)
    {
        return new Report(message);
    }
}