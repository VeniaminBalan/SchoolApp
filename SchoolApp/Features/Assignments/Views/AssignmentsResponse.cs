using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Subjects.View;

namespace SchoolApp.Features.Assignments.Views;

public class AssignmentsResponse
{
    public string id { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
    public SubjectResponseForAssignment Subject { get; set; }
}