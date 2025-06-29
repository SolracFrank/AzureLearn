using Domain.Dtos.Requests.Users;
using Infrastructure;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Validation.Students;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class StudentsController : ControllerBase
{
    private readonly IUnitOfWork  _unitOfWork;

    public StudentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet] 
    public async Task<ActionResult<List<Student>>> GetAll(CancellationToken cancellationToken)
    {
        var students = await _unitOfWork.StudentsRepository.GetAllAsync(cancellationToken);
        return Ok(students);
    }
    [HttpPost("[action]")] 
    public async Task<ActionResult<List<Student>>> Create([FromBody] CreateStudentRequest request,CancellationToken cancellationToken)
    {
        var validation = new CreateStudentValidation(_unitOfWork);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var newStudent = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
        };
        await _unitOfWork.StudentsRepository.AddAsync(newStudent, cancellationToken);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        if(!result) throw new ApplicationException("Unable to create student");
        
        return Ok(newStudent);
    }
}