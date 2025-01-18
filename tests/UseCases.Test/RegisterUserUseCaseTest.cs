using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repository;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UserCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;


namespace UseCases.Test;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }
    
    [Fact]
    public async Task Error_Email_Alredy_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);
        
        Func<Task> act = async () => await useCase.Execute(request);
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages[0].Contains("Email already exists"));
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        
        var useCase = CreateUseCase();
        
        Func<Task> act = async () => await useCase.Execute(request);
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages[0].Contains(ResourcesMessagesExceptions.NAME_EMPTY));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordHashed = PasswordEncripterBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        
        if(string.IsNullOrEmpty(email) == false)
            readRepositoryBuilder.ExistsActiveUserWithEmail(email);
        
        return new RegisterUserUseCase(readRepositoryBuilder.Build(), writeRepository, mapper, passwordHashed, unitOfWork);
    }
}