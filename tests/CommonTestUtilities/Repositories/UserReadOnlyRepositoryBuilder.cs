using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repository;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();
    
    public IUserReadOnlyRepository Build() => _repository.Object;
}