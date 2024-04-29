using AutoMapper;
using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users;
using Backend.DAL.IRepositories;
using Serilog;

namespace Backend.BLL.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public Guid CreateUser(CreateUserRequest request)
    {
        _logger.Information("Вызываем метод репозитория");
        var user = _mapper.Map<UserDto>(request);
        return _usersRepository.CreateUser(user);
    }

    public void DeleteUserById(Guid id)
    {
        _logger.Information("Вызываем метод репозитория");
        var user=_usersRepository.GetUserById(id);
        if (user is null)
        {
            _logger.Error($"Пользователь c id:{id} не найден");
            throw new NotFoundException($"Пользователь c id:{id} не найден");
        }
        _usersRepository.DeleteUser(user);
    }

    public List<UserResponse> GetAllUsers()
    {
        _logger.Information("Вызываем метод репозитория");
        List<UserDto> userDtos = _usersRepository.GetAllUsers();
        return _mapper.Map<List<UserResponse>>(userDtos);
    }

    public UserWithDevicesResponse GetUserById(Guid id)
    {
        _logger.Information("Вызываем метод репозитория");
        UserDto userDto= _usersRepository.GetUserById(id);
        var user = _mapper.Map<UserWithDevicesResponse>(userDto);
        if (user is null)
        {
            _logger.Error($"Пользователь c id:{id} не найден");
            throw new NotFoundException($"Пользователь c id:{id} не найден");
        }
        return user;
    }

    public Guid UpdateUser(UpdateUserRequest request)
    {
        _logger.Information("Вызываем метод репозитория");
        var user = _usersRepository.GetUserById(request.Id);
        if (user is null)
        {
            _logger.Error($"Пользователь c id:{request.Id} не найден");
            throw new NotFoundException($"Пользователь c id:{request.Id} не найден");
        }

        user.UserName= request.UserName;
        user.Password= request.Password;

        return _usersRepository.UpdateUser(user);
    }
}
