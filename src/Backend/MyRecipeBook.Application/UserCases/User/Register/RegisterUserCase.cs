using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UserCases.User.Register;

public class RegisterUserCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }
    
    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
    
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage);
            throw new Exception();
        }
    }
}

