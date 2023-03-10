using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Database;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Assignments.Views;
using SchoolApp.Features.Subjects.View;

namespace SchoolApp.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public AssignmentsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentModel>> Add(string subjectName, AssignmentsRequest request)
    {   
        var subject = await _appDbContext.Subjects.FirstOrDefaultAsync(x => subjectName == x.Name);
        if (subject is null) return NotFound("Subject does not exist");
        
        var assignment = new AssignmentModel
        {
            id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = subject,
            Description = request.Description,
            DeadLine = request.DeadLine
        };

        assignment = (await _appDbContext.Assignments.AddAsync(assignment)).Entity;
        await _appDbContext.SaveChangesAsync();
       
        var res = new AssignmentsResponseForSubject()
        {
            id = assignment.id,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };

        return Created("assignment", res);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentsResponse>>> Get()
    {
        var assignments = await _appDbContext.Assignments.Select(
            assignment => new AssignmentsResponse
            {
                id = assignment.id,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine,
                Grade = assignment.Grade,
                Subject = new SubjectResponseForAssignment
                {
                    id = assignment.Subject.id,
                    Name = assignment.Subject.Name,
                    ProffesorMail = assignment.Subject.ProffesorMail
                }
            }).ToListAsync();

        return Ok(assignments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentsResponse>>  Get([FromRoute] string id)
    {
        var assignment = await _appDbContext.Assignments
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => id == x.id);
        if (assignment is null) return NotFound("Assignment does not exist");
        
        var res = new AssignmentsResponse
        {
            id = assignment.id,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine,
            Grade = assignment.Grade,
            Subject = new SubjectResponseForAssignment
            {
                id = assignment.Subject.id,
                Name = assignment.Subject.Name,
                ProffesorMail = assignment.Subject.ProffesorMail
            }
        };
        
        return Ok(res);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<AssignmentsResponse>>  Patch([FromRoute] string id,AssignmentsRequest request)
    {
        var assignment = await _appDbContext.Assignments.Include(x => x.Subject).FirstOrDefaultAsync(x => x.id == id);
        if (assignment is null) return NotFound("Assignment does not exist");
        
        assignment.Updated = DateTime.UtcNow;
        assignment.Description = request.Description;
        assignment.DeadLine = request.DeadLine;
        assignment.Grade = request.Grade;

        await _appDbContext.SaveChangesAsync();
        
        return Ok(new AssignmentsResponse()
        {
            id = assignment.id,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine,
            Grade = assignment.Grade,
            Subject = new SubjectResponseForAssignment
                {
                    id = assignment.Subject.id,
                    Name = assignment.Subject.Name,
                    ProffesorMail = assignment.Subject.ProffesorMail
                },
        });
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<AssignmentsResponse>> Delete([FromRoute] string id)
    {
        var assignment = await _appDbContext.Assignments.FirstOrDefaultAsync(x => id == x.id);
        if (assignment is null) return NotFound("Assignment does not exist");

        _appDbContext.Remove(assignment);
        await _appDbContext.SaveChangesAsync();

        return Ok($"Object {assignment.id} was deleted successfully");
    }
}
