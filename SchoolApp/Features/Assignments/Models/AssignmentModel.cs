using SchoolApp.Base;

namespace SchoolApp.Features.Assignments.Models;

public class AssignmentModel : Model
{
    public SubjectModel Subject { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine  { get; set; }

    public decimal Grade { get; set; }
}