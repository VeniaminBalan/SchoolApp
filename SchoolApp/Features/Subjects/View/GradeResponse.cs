namespace SchoolApp.Features.Subjects.View;

public enum GradeType {
    None,
    Assignment,
    Test
}
public class GradeResponse
{
    public GradeType Type { get; set; }
    public string Description { get; set; }
    public decimal Grade { get; set; }
}