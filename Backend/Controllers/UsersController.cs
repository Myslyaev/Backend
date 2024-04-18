using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("/users")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public List<UserDto> GetAllUsers()
    {
        return _usersService.GetAllUsers();
    }

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id)
    {
        return _usersService.GetUserById(Guid.NewGuid());
    }
}
