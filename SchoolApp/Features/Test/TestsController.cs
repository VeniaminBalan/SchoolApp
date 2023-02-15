using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Database;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Subjects.View;
using SchoolApp.Features.Test.Views;

namespace SchoolApp.Features.Test;

[ApiController]
[Route("tests")]
public class TestsController : ControllerBase
{
    //private static List<TestModel> _mockDB = new List<TestModel>();
    private readonly AppDbContext _appDbContext;

    public TestsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [HttpPost]
    public async Task<ActionResult<TestsResponse>> Add(string subjectName, TestsRequest request)
    {
        var subject = await _appDbContext.Subjects
            .FirstOrDefaultAsync(x => subjectName == x.Name);
        if (subject is null) return NotFound("Subject does not exist");
        
        var test = new TestModel()
        {
            id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Description = request.Description,
            Grade = request.Grade,
            Subject = subject
        };
        
        test = (await _appDbContext.Tests.AddAsync(test)).Entity;
        await _appDbContext.SaveChangesAsync();
        
        return Ok(
                new TestsResponse()
                {
                    id = test.id,
                    Description = test.Description,
                    Grade = test.Grade,
                    Subject = new SubjectResponseForTest
                    {
                        id = test.Subject.id,
                        Name = test.Subject.Name,
                        ProffesorMail = test.Subject.ProffesorMail
                    }
                }
            );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestsResponse>>> Get()
    {
        var tests = await _appDbContext.Tests.Select(
                test => new TestsResponse
                {
                    id = test.id,
                    Description = test.Description,
                    Grade = test.Grade,
                    Subject = new SubjectResponseForTest
                    {
                        id = test.Subject.id,
                        Name = test.Subject.Name,
                        ProffesorMail = test.Subject.ProffesorMail
                    }
                }).ToListAsync();

        return Ok(tests);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TestsResponse>> Get([FromRoute] string id)
    {
        var test = await _appDbContext.Tests
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(t => id == t.id);
        if (test is null) return NotFound("The test does not exist");


        return Ok(new TestsResponse
        {
            id = test.id,
            Description = test.Description,
            Grade = test.Grade,
            Subject = new SubjectResponseForTest
            {
                id = test.Subject.id,
                Name = test.Subject.Name,
                ProffesorMail = test.Subject.ProffesorMail
            }
        });

    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<TestsResponse>> Delete([FromRoute] string id)
    {
        var test = await _appDbContext.Tests
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(t => id == t.id);
        if (test is null) return NotFound("The test does not exist");

        _appDbContext.Remove(test);
        await _appDbContext.SaveChangesAsync();

        return Ok($"Object {test.id} was deleted successfully");
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<TestsResponse>> Patch([FromRoute] string id,TestsRequest request)
    {
        var test = await _appDbContext.Tests
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(t => id == t.id);
        if (test is null) return NotFound("The test does not exist");
        
        test.Updated = DateTime.UtcNow;
        test.Description = request.Description;
        test.Grade = request.Grade;
        
        await _appDbContext.SaveChangesAsync();

        return Ok(new TestsResponse
        {
            id = test.id,
            Description = test.Description,
            Grade = test.Grade,
            Subject = new SubjectResponseForTest
            {
                id = test.Subject.id,
                Name = test.Subject.Name,
                ProffesorMail = test.Subject.ProffesorMail
            }
        });
    }
}