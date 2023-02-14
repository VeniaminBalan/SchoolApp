using SchoolApp.Base;

namespace SchoolApp.Features.Assignments.Models;

public class TestModel : Model
{
    public string Description { get; set; }

    public SubjectModel Subject { get; set; }
    
    public decimal Grade { get; set; }
}