using AutoMapper;
using Backend.BLL.IServices;
using Backend.Core.Constants;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users;
using Backend.DAL.IRepositories;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.BLL.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;
    private readonly string _pepper;
    private readonly int _iteration = 3;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _pepper = (Environment.GetEnvironmentVariable("Pepper"));
    }

    public Guid CreateUser(CreateUserRequest request)
    {
        _logger.Information("Вызываем метод репозитория");
        var user = _mapper.Map<UserDto>(request);
        user.PasswordSalt = PasswordHasher.GenerateSalt();
        user.PasswordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);
        return _usersRepository.CreateUser(user);
    }

    public AuthenticatedResponse LoginUser(LoginUserRequest request)
    {
        UserDto user = _mapper.Map<UserDto>(request);

        _logger.Information("Вызываем метод репозитория");
        var userDb = _usersRepository.GetUserByMail(user.Email.ToLower());

        if (userDb == null)
            throw new Exception("Логин или пароль введены неверно");

        _logger.Information("Проверяем аутентификационные данные");
        var passwordHash = PasswordHasher.ComputeHash(request.Password, userDb.PasswordSalt, _pepper, _iteration);
        if (userDb.PasswordHash != passwordHash)
            throw new Exception("Логин или пароль введены неверно");

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretKey")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: ServicesConstants.ProjectName,
            audience: ServicesConstants.Ui,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddHours(24),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return new AuthenticatedResponse { Token = tokenString };
    }

    public void DeleteUserById(Guid id)
    {
        _logger.Information("Вызываем метод репозитория");
        var user = _usersRepository.GetUserById(id);
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
        UserDto userDto = _usersRepository.GetUserById(id);
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

        //user.UserName= request.UserName;
        //user.Password= request.Password;

        return _usersRepository.UpdateUser(user);
    }
}
