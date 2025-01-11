using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UserCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {
        var useCase = new RegisterUserCase();
        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }
}