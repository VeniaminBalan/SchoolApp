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
                Grades = new List<GradeResponse>()
                    .Concat(subject.Assignments.Select(a => 
                            new GradeResponse
                            {
                                Type = View.GradeType.Assignment,
                                Description = a.Description,
                                Grade = a.Grade,
                            }
                        ).ToList()
                    )
                    .Concat(subject.Tests.Select(t => 
                            new GradeResponse
                            {
                                Type = View.GradeType.Test,
                                Description = t.Description,
                                Grade= t.Grade,
                            }
                        ).ToList()
                    ),
                Assignment = subject.Assignments.Select(
                    assignment => new AssignmentsResponseForSubject
                    {
                        DeadLine = assignment.DeadLine,
                        Description = assignment.Description,
                        id = assignment.id,
                        Grade = assignment.Grade
                    }),
                Tests = subject.Tests.Select(
                    test => new TestResponseForSubject
                    {
                        id = test.id,
                        Description = test.Description,
                        Grade = test.Grade
                    })
            }).ToListAsync();

        return Ok(subjects);

    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectResponse>> Get([FromRoute] string id)
    {
        var subject = await _appDbContext.Subjects
            .Include(t => t.Tests)
            .Include(a => a.Assignments)
            .FirstOrDefaultAsync(s => s.id == id);
        
        if (subject is null) return NotFound("subject does not exist");

        var res = new SubjectResponse
        {
            id = subject.id,
            Name = subject.Name,
            ProffesorMail = subject.ProffesorMail,
            Grades = new List<GradeResponse>()
                .Concat(subject.Assignments.Select(a => 
                        new GradeResponse
                        {
                            Type = View.GradeType.Assignment,
                            Description = a.Description,
                            Grade = a.Grade,
                        }
                    ).ToList()
                )
                .Concat(subject.Tests.Select(t => 
                        new GradeResponse
                        {
                            Type = View.GradeType.Test,
                            Description = t.Description,
                            Grade= t.Grade,
                        }
                    ).ToList()
                ),
            Assignment = subject.Assignments.Select(
                assignment => new AssignmentsResponseForSubject
                {
                    DeadLine = assignment.DeadLine,
                    Description = assignment.Description,
                    id = assignment.id,
                    Grade = assignment.Grade
                }),
            Tests = subject.Tests.Select(
                test => new TestResponseForSubject
                {
                    id = test.id,
                    Description = test.Description,
                    Grade = test.Grade
                })
        };

        return Ok(res);
    }    
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<SubjectResponse>> Patch([FromRoute] string id,SubjectRequest request)
    {
        var subject = await _appDbContext.Subjects
            .Include(t => t.Tests)
            .Include(a => a.Assignments)
            .FirstOrDefaultAsync(s => s.id == id);
        
        if (subject is null) return NotFound("subject does not exist");
        
        subject.Updated = DateTime.UtcNow;
        subject.Name = request.Name;
        subject.ProffesorMail = request.ProffesorMail;
        
        await _appDbContext.SaveChangesAsync();

        var res = new SubjectResponse
        {
            id = subject.id,
            Name = subject.Name,
            ProffesorMail = subject.ProffesorMail,
            Grades = new List<GradeResponse>()
                .Concat(subject.Assignments.Select(a => 
                        new GradeResponse
                        {
                            Type = View.GradeType.Assignment,
                            Description = a.Description,
                            Grade = a.Grade,
                        }
                    ).ToList()
                )
                .Concat(subject.Tests.Select(t => 
                        new GradeResponse
                        {
                            Type = View.GradeType.Test,
                            Description = t.Description,
                            Grade= t.Grade,
                        }
                    ).ToList()
                ),
            Assignment = subject.Assignments.Select(
                assignment => new AssignmentsResponseForSubject
                {
                    DeadLine = assignment.DeadLine,
                    Description = assignment.Description,
                    id = assignment.id,
                    Grade = assignment.Grade
                }),
            Tests = subject.Tests.Select(
                test => new TestResponseForSubject
                {
                    id = test.id,
                    Description = test.Description,
                    Grade = test.Grade
                })
        };

        return Ok(res);
    }

    
    [HttpDelete("{id}")]
    public async Task<ActionResult<SubjectResponse>> Delete([FromRoute] string id)
    {
        var subject = await _appDbContext.Subjects
            .Include(x => x.Tests)
            .Include(y => y.Assignments)
            .FirstOrDefaultAsync(s => s.id == id);
        if (subject is null) return NotFound("subject does not exist");

        subject.Assignments.Select(a => subject.Assignments.Remove(a));
        subject.Tests.Select(t => subject.Tests.Remove(t));
        _appDbContext.Remove(subject);
        
        await _appDbContext.SaveChangesAsync();

        return Ok($"Object {subject.id} was deleted successfully");
    }
    
}