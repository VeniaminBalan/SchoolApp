using System.ComponentModel.DataAnnotations;
using SchoolApp.Base;

namespace SchoolApp.Features.Subjects.View;

public class SubjectRequest
{
    [Required]public string Name { get; set; }
    
    [EmailAddress]
    [Required]
    public string ProffesorMail { get; set; }
}