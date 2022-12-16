using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Assignments.Views;

public class AssignmentsRequest
{
    [Required]public string Subject { get; set; }
    
    [Required]public string Description { get; set; }
    
    [Required]public DateTime DeadLine { get; set; }
}