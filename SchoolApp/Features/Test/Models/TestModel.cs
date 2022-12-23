using SchoolApp.Base;

namespace SchoolApp.Features.Assignments.Models;

public class TestModel : Model
{
    public string Subject { get; set; }

    public DateTime TestDate { get; set; }
}