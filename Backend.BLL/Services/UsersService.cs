﻿using AutoMapper;
using Backend.BLL.IServices;
using Backend.Core.Constants;
using Backend.Core.Constants.Exceptions;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users;
using Backend.DAL.IRepositories;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ValidationException = Backend.Core.Exceptions.ValidationException;

namespace Backend.BLL.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserRequest> _userValidator;
    private readonly IValidator<UpdateUserRequest> _userUpdateValidator;
    private readonly string _pepper;
    private readonly int _iteration;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, IValidator<CreateUserRequest> userValidator, IValidator<UpdateUserRequest> userUpdateValidator)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _pepper = (Environment.GetEnvironmentVariable("Pepper"));
        _iteration = ServicesConstants.Iteration;
        _userValidator = userValidator;
        _userUpdateValidator = userUpdateValidator;
    }

    public Guid CreateUser(CreateUserRequest request)
    {
        var validationResult = _userValidator.Validate(request);
        if (validationResult.IsValid)
        {
            _logger.Information("Вызываем метод репозитория");
            var user = _mapper.Map<UserDto>(request);
            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);
            return _usersRepository.CreateUser(user);
        }
        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);

        throw new ValidationException(exceptions);
    }

    public AuthenticatedResponse LoginUser(LoginUserRequest request)
    {
        UserDto user = _mapper.Map<UserDto>(request);

        _logger.Information("Вызываем метод репозитория");
        var userDb = _usersRepository.GetUserByMail(user.Email.ToLower());

        if (userDb == null)
            throw new AuthenticationException(string.Format(UsersServiceExceptions.AuthenticationException));

        _logger.Information("Проверяем аутентификационные данные");
        var passwordHash = PasswordHasher.ComputeHash(request.Password, userDb.PasswordSalt, _pepper, _iteration);
        if (userDb.PasswordHash != passwordHash)
            throw new AuthenticationException(string.Format(UsersServiceExceptions.AuthenticationException));

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
            throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, id));
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
            throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, id));
        }
        return user;
    }

    public Guid UpdateUser(UpdateUserRequest request)
    {
        var validationResult = _userUpdateValidator.Validate(request);
        if (validationResult.IsValid)
        {
            _logger.Information("Вызываем метод репозитория");
            var userDb = _usersRepository.GetUserById(request.Id);
            if (userDb is null)
            {
                _logger.Error($"Пользователь c id:{request.Id} не найден");
                throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, request.Id));
            }

            //user.UserName= request.UserName;
            //user.Password= request.Password;

            return _usersRepository.UpdateUser(userDb);
        }
        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);

        throw new ValidationException(exceptions);
    }
}
