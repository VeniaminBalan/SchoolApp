using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Subjects.View;

public class SubjectRequest
{
    [Required]public string Name { get; set; }
    
    [EmailAddress]
    [Required]
    public string ProffesorMail { get; set; }
    [Required]public IEnumerable<Double> Grades { get; set; }
}