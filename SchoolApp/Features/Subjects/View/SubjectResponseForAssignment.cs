using SchoolApp.Base;
using SchoolApp.Features.Assignments.Views;
using SchoolApp.Features.Test.Views;

namespace SchoolApp.Features.Subjects.View;

public class SubjectResponseForAssignment
{
    public string id { get; set; }
    public string Name { get; set; }
    public string ProffesorMail { get; set; }
    public IEnumerable<Grade> Grades { get; set; }
    public IEnumerable<TestsResponse> Tests { get; set; }
}