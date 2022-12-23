namespace SchoolApp.Features.Subjects.View;

public class SubjectResponse
{
    public string id { get; set; }
    public string Name { get; set; }
    public string ProffesorMail { get; set; }
    public IEnumerable<Double> Grades { get; set; }
}