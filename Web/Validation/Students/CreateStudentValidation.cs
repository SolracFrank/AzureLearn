using Domain.Dtos.Requests.Users;
using FluentValidation;
using Infrastructure.Repositories.Interfaces;

namespace Web.Validation.Students;

public class CreateStudentValidation : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentValidation(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress()
            .MustAsync(async(email,ct)=> await unitOfWork.StudentsRepository.FirstOrDefaultAsync(x => x.Email == email,ct) == null )
            .WithMessage("Email is taken.");
            
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
    }
}