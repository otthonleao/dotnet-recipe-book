using AutoMapper;
using MyRecipeBook.Application.Services.Cryptograhy;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UserCases.User.Register;

public class RegisterUserCase : IRegisterUserCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly PasswordSecurityService _hashedPassword;
    private readonly IUnitWork _unitWork;
    
    public RegisterUserCase(IUserReadOnlyRepository readOnlyRepository, IUserWriteOnlyRepository writeOnlyRepository, IMapper mapper, PasswordSecurityService hashedPassword, IUnitWork unitWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _mapper = mapper;
        _hashedPassword = hashedPassword;
        _unitWork = unitWork;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _hashedPassword.GenerateHash(request.Password);
        
        await _writeOnlyRepository.Add(user);
        await _unitWork.Commit();
        
        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }
    
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        
        var emailExist = await _readOnlyRepository.ExistsActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Email already exists"));
        }
    
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}

