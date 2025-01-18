using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repository;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UserCases.User.Register;


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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var passwordHashed = PasswordEncripterBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().Build();
        
        return new RegisterUserUseCase(readRepository, writeRepository, mapper, passwordHashed, unitOfWork);
    }
}