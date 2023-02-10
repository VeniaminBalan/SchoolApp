using SchoolApp.Features.Assignments.Models;

namespace SchoolApp.Features.Assignments.Views;

public class AssignmentsResponse
{
    public string id { get; set; }
    
    public SubjectModel Subject { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DeadLine { get; set; }
}