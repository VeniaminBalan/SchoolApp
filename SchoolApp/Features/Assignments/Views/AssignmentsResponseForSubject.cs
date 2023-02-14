namespace SchoolApp.Features.Assignments.Views;

public class AssignmentsResponseForSubject
{
    public string id { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
    public decimal Grade { get; set; }
}