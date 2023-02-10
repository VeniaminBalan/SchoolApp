using SchoolApp.Base;

namespace SchoolApp.Features.Assignments.Models;

public class TestModel : Model
{
    public string Title { get; set; }

    public DateTime TestDate { get; set; }
    
    public List<SubjectModel> Subjects { get; set; }
}