using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UserCases.User.Register;

public interface IRegisterUserCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}