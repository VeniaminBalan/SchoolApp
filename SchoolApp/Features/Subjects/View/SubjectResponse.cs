using SchoolApp.Base;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Assignments.Views;
using SchoolApp.Features.Test.Views;

namespace SchoolApp.Features.Subjects.View;

public class SubjectResponse
{
    public string id { get; set; }
    public string Name { get; set; }
    public string ProffesorMail { get; set; }
    public IEnumerable<GradeResponse> Grades { get; set; }
    public IEnumerable<AssignmentsResponseForSubject> Assignment { get; set; }
    public IEnumerable<TestResponseForSubject> Tests { get; set; }
}