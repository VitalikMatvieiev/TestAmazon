using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace TestAmazon.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IDynamoDBContext _context;

    public StudentsController(IDynamoDBContext context)
    {
        _context = context;
    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetById(int studentId)
    {
        var student = await _context.LoadAsync<DynamoDb.Student>(studentId);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var student = await _context.ScanAsync<DynamoDb.Student>(default).GetRemainingAsync();
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(DynamoDb.Student studentRequest)
    {
        var student = await _context.LoadAsync<DynamoDb.Student>(studentRequest.Id);
        if (student != null) return BadRequest($"Student with Id {studentRequest.Id} Already Exists");
        await _context.SaveAsync(studentRequest);
        return Ok(studentRequest);
    }

    [HttpDelete("{studentId}")]
    public async Task<IActionResult> DeleteStudent(int studentId)
    {
        var student = await _context.LoadAsync<DynamoDb.Student>(studentId);
        if (student == null) return NotFound();
        await _context.DeleteAsync(student);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(DynamoDb.Student studentRequest)
    {
        var student = await _context.LoadAsync<DynamoDb.Student>(studentRequest.Id);
        if (student == null) return NotFound();
        await _context.SaveAsync(studentRequest);
        return Ok(studentRequest);
    }
}