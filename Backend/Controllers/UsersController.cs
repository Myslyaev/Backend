using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Backend.Core.Models.Devices;
using Backend.Core.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.API.Controllers;

[ApiController]
[Route("/api/users")]

public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public UsersController(IUsersService usersService, IDevicesService devicesService)
    {
        _usersService = usersService;
        _devicesService= devicesService;
    }

    [HttpGet]
    public ActionResult<List<UserResponse>> GetAllUsers()
    {
        _logger.Information("Получаем список всех пользователей");
        return Ok(_usersService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<UserWithDevicesResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем пользователя по {id}");
        return Ok(_usersService.GetUserById(id));
    }

    [HttpPost]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information($"Добавляем пользователя {request.Email}");
        return Ok(_usersService.CreateUser(request));
    }

    [HttpPost("{userId}/devices")]
    public ActionResult<Guid> CreateDeviceWithOwner([FromBody] CreateDeviceWithOwnerRequest request)
    {
        _logger.Information($"Добавляем устройство {request.Name} принадлежащее {request.OwnerId}");
        return Ok(_devicesService.CreateDeviceWitnOwner(request));
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
    {
        _logger.Information($"Изменяем инфорфацию о пользователе {request.Id}");
        return Ok(_usersService.UpdateUser(request));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _logger.Information($"Удаляем пользователя {id}");
        _usersService.DeleteUserById(id);
        return Ok();
    }
}
