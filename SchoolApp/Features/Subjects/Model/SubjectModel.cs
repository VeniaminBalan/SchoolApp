using SchoolApp.Base;

namespace SchoolApp.Features.Assignments.Models;

public class SubjectModel : Model
{
    public string Name { get; set; }
    public string ProffesorMail { get; set; }
    public IEnumerable<Double> Grades { get; set; }
}