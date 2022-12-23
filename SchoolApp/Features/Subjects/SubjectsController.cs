using Microsoft.AspNetCore.Mvc;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Subjects.View;

namespace SchoolApp.Features.Subjects;

[ApiController]
[Route("subjects")]
public class SubjectsController : ControllerBase
{
    private static List<SubjectModel> _mockDB = new List<SubjectModel>();

    public SubjectsController() {}
    
    [HttpPost]
    public SubjectResponse Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProffesorMail = request.ProffesorMail,
            Grades = request.Grades
        };
        
        _mockDB.Add(subject);
        return new SubjectResponse
        {
            id = subject.id,
            Name = subject.Name,
            ProffesorMail = subject.ProffesorMail,
            Grades = subject.Grades
        };
        
    }
    
    [HttpGet]
    public IEnumerable<SubjectResponse> Get()
    {
        return _mockDB.Select(
            subject => new SubjectResponse
            {
                id = subject.id,
                Name = subject.Name,
                ProffesorMail = subject.ProffesorMail,
                Grades = subject.Grades
            }).ToList();
    }
    
    [HttpGet("{id}")]
    public SubjectResponse Get([FromRoute] string id)
    {
        var subjcet = _mockDB.FirstOrDefault(x => x.id == id);
        if (subjcet is null) return null;

        return new SubjectResponse
        {
            id = subjcet.id,
            Name = subjcet.Name,
            ProffesorMail = subjcet.ProffesorMail,
            Grades = subjcet.Grades
        };
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
        var subject = _mockDB.FirstOrDefault(x => x.id == id);
        if (subject is null) return null;

        _mockDB.Remove(subject);

        return Ok($"Object {subject.id} was deleted successfully");
    }
    
    [HttpPatch("{id}")]
    public SubjectResponse Patch([FromRoute] string id,SubjectRequest request)
    {
        var subject = _mockDB.FirstOrDefault(x => x.id == id);
        if (subject is null) return null;
        
        subject.Updated = DateTime.UtcNow;
        subject.Name = request.Name;
        subject.ProffesorMail = request.ProffesorMail;
        subject.Grades = request.Grades;
        
        return new SubjectResponse()
        {
            id = subject.id,
            Name = subject.Name,
            ProffesorMail = subject.ProffesorMail,
            Grades = subject.Grades,
        };
    }
    
}