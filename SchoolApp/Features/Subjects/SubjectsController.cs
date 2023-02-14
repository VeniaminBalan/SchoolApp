using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Database;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Assignments.Views;
using SchoolApp.Features.Subjects.View;
using SchoolApp.Features.Test.Views;

namespace SchoolApp.Features.Subjects;

[ApiController]
[Route("subjects")]
public class SubjectsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public SubjectsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [HttpPost]
    public async Task<ActionResult<SubjectResponse>> Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProffesorMail = request.ProffesorMail,
        };
        
        subject = (await _appDbContext.Subjects.AddAsync(subject)).Entity;
        await _appDbContext.SaveChangesAsync();
        
        return new SubjectResponse
        {
            id = subject.id,
            Name = subject.Name,
            ProffesorMail = subject.ProffesorMail,
        };
        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubjectResponse>>> Get()
    {
        var subjects = await _appDbContext.Subjects
            .Include(x =>x.Tests)
            .Include(y=>y.Assignments)
            .Select(
            subject => new SubjectResponse
            {
                id = subject.id,
                Name = subject.Name,
                ProffesorMail = subject.ProffesorMail,
                //Grades = subject.Assignments.Select(s => s.id == subject.id)
                Assignment = subject.Assignments.Select(
                    assignment => new AssignmentsResponseForSubject
                    {
                        DeadLine = assignment.DeadLine,
                        Description = assignment.Description,
                        id = assignment.id,
                        Grade = assignment.Grade
                    }),
                Tests = subject.Tests.Select(
                    test => new TestsResponse
                    {
                        id = test.id,
                        Description = test.Description,
                        Grade = test.Grade
                    })
            }).ToListAsync();

        return Ok(subjects);

    }
    /*
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
        //subject.Grades = request.Grades;
        
        return new SubjectResponse()
        {
            id = subject.id,
            Name = subject.Name,
            ProffesorMail = subject.ProffesorMail,
            Grades = subject.Grades,
        };
    }
    */
}