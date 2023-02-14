using Microsoft.AspNetCore.Mvc;
using SchoolApp.Database;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Test.Views;

namespace SchoolApp.Features.Test;

[ApiController]
[Route("tests")]
public class TestsController : ControllerBase
{
    private static List<TestModel> _mockDB = new List<TestModel>();
    private readonly AppDbContext _appDbContext;

    public TestsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [HttpPost]
    public TestsResponse Add(TestsRequest request)
    {
        var test = new TestModel()
        {
            id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Title = request.Title,
            TestDate = DateTime.UtcNow
        };
        
        _mockDB.Add(test);
        
        return new TestsResponse()
        {
            id = test.id,
            Title = test.Title,
            TestDate = test.TestDate
        };
        
    }

    [HttpGet]
    public IEnumerable<TestsResponse> Get()
    {
        return _mockDB.Select(
            test => new TestsResponse
            {
                id = test.id,
                Title = test.Title,
                TestDate = test.TestDate
            }
        ).ToList();
    }
    
    [HttpGet("{id}")]
    public TestsResponse Get([FromRoute] string id)
    {
        var test = _mockDB.FirstOrDefault(x => x.id == id);
        if (test is null) return null;

        return new TestsResponse
        {
            id = test.id,
            Title = test.Title,
            TestDate = test.TestDate
        };
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
        var test = _mockDB.FirstOrDefault(x => x.id == id);
        if (test is null) return null;

        _mockDB.Remove(test);

        return Ok($"Object {test.id} was deleted successfully");
    }
    
    [HttpPatch("{id}")]
    public TestsResponse Patch([FromRoute] string id,TestsRequest request)
    {
        var test = _mockDB.FirstOrDefault(x => x.id == id);
        if (test is null) return null;
        
        test.Updated = DateTime.UtcNow;
        test.Title = request.Title;
        test.TestDate = request.TestDate; 
        
        return new TestsResponse()
        {
            id = test.id,
            Title = test.Title,
            TestDate = test.TestDate,
        };
    }
}