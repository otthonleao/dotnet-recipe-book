using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UserCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage(ResourcesMessagesExceptions.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");
        RuleFor(user => user.Password.Length)
            .NotEmpty().WithMessage("Password is required")
            .GreaterThanOrEqualTo(6).WithMessage("Password must have at least 6 characters");
    }
}