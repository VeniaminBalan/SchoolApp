using SchoolApp.Base;

namespace SchoolApp.Features.Assignments.Models;

public class SubjectModel : Model
{
    public string Name { get; set; }
    public string ProffesorMail { get; set; }
    public IList<Grade> Grades { get; set; }
    
    public IList<AssignmentModel> Assignments { get; set; }
    
    public IList<TestModel> Tests { get; set; }
}