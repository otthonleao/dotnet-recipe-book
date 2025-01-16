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
            .NotEmpty().WithMessage("Email is required");
        When(user => string.IsNullOrEmpty(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage("Email is invalid");
        });

        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
        When(user => !string.IsNullOrEmpty(user.Password), () =>
        {
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6)
                .WithMessage("Password must have at least 6 characters");
        });
    }
}