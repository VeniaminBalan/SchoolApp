using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Subjects.View;

namespace SchoolApp.Features.Test.Views;

public class TestsResponse
{
    public string id { get; set; }
    public string Description { get; set; }

    public SubjectResponseForTest Subject { get; set; }
    public decimal Grade { get; set; }
    
}