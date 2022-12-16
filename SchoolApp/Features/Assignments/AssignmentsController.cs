using Microsoft.AspNetCore.Mvc;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Assignments.Views;

namespace SchoolApp.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentsController : ControllerBase
{
    private static List<AssignmentModel> _mockDB = new List<AssignmentModel>();

    public AssignmentsController() {}

    [HttpPost]
    public AssignmentsResponse Add(AssignmentsRequest request)
    {
        var assignment = new AssignmentModel
        {
            id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            Description = request.Description,
            DeadLine = request.DeadLine
        };
        
        _mockDB.Add(assignment);
        return new AssignmentsResponse
        {
            id = assignment.id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
        
    }

    [HttpGet]
    public IEnumerable<AssignmentsResponse> Get()
    {
        return _mockDB.Select(
            assignment => new AssignmentsResponse
            {
                id = assignment.id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine
            }).ToList();
    }

    [HttpGet("{id}")]
    public AssignmentsResponse Get([FromRoute] string id)
    {
        var assignment = _mockDB.FirstOrDefault(x => x.id == id);
        if (assignment is null) return null;
        return new AssignmentsResponse
        {
            id = assignment.id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }
}
//functie delete/patch -> 
//[HttpDelete]
//[HttpPatch]