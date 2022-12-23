using Microsoft.AspNetCore.JsonPatch;
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

    [HttpPatch("{id}")]
    public AssignmentsResponse Patch([FromRoute] string id,AssignmentsRequest request)
    {
        var assignment = _mockDB.FirstOrDefault(x => x.id == id);
        if (assignment is null) return null;
        
        assignment.Updated = DateTime.UtcNow;
        assignment.Subject = request.Subject;
        assignment.Description = request.Description;
        assignment.DeadLine = request.DeadLine;
        
        return new AssignmentsResponse()
        {
            id = assignment.id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }
    
    /*[HttpPatch("{id}")]
    public AssignmentsResponse Patch([FromRoute] string id, [FromBody] JsonPatchDocument<AssignmentModel> patch )
    {
        var fromDb = _mockDB.FirstOrDefault(x => x.id == id);
        
        if (fromDb is null) return null;

        var original = new AssignmentModel
        {
            id= fromDb.id,
            Created = fromDb.Created,
            Updated = fromDb.Updated,
            Subject= fromDb.Subject,
            Description=  fromDb.Description,
            DeadLine = fromDb.DeadLine
        };
        
        patch.ApplyTo(fromDb, ModelState);
        
        var isValid = TryValidateModel(fromDb);
        
        if(!isValid)
        {
            return null;
        }
        
        fromDb.Updated = DateTime.UtcNow;
        fromDb.Subject = original.Subject;
        fromDb.Description = original.Description;
        fromDb.DeadLine = original.DeadLine;
        
        return new AssignmentsResponse()
        {
            id = fromDb.id,
            Subject = fromDb.Subject,
            Description = fromDb.Description,
            DeadLine = fromDb.DeadLine
        };
    }*/

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
        var assignment = _mockDB.FirstOrDefault(x => x.id == id);
        if (assignment is null) return null;

        _mockDB.Remove(assignment);

        return Ok($"Object {assignment.id} was deleted successfully");
    }
}
