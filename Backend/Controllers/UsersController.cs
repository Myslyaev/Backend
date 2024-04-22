using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("/api/users")]

public class UsersController : Controller
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public ActionResult<List<UserDto>> GetAllUsers()
    {
        try
        {
            var result = _usersService.GetAllUsers();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("{id}")]
    public ActionResult<UserDto> GetUserById(Guid id)
    {
        var result = _usersService.GetUserById(id);
        return Ok(result);
    }

    [HttpPost]
    public ActionResult CreateUser([FromQuery] UserDto user)
    {
        try
        {
            _usersService.CreateUser(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return NoContent();

    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser(Guid id, [FromQuery] UserDto user)
    {
        try
        {
            _usersService.UpdateUser(id, user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        try
        {
            _usersService.DeleteUserById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        return NoContent();
    }
}
