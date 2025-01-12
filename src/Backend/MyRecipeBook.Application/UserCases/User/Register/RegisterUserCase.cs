using Microsoft.Extensions.Options;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UserCases.User.Register;

public class RegisterUserCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var autoMapper = new AutoMapper.MapperConfiguration(Options =>
        {
            Options.AddProfile(new AutoMapping());
        }).CreateMapper();
        
        var user = autoMapper.Map<Domain.Entities.User>(request);
        
        // criptografia de senha
        
        // salvar no banco de dados
        
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
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}

