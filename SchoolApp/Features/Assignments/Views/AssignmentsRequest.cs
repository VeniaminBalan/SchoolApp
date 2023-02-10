using System.ComponentModel.DataAnnotations;
using SchoolApp.Features.Assignments.Models;

namespace SchoolApp.Features.Assignments.Views;

public class AssignmentsRequest
{
    //[Required]public SubjectModel Subject { get; set; }
    
    [Required]public string Description { get; set; }
    
    [Required]public DateTime DeadLine { get; set; }
}