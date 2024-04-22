using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.DAL.IRepositories;

namespace Backend.BLL.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public void CreateUser(UserDto user)
    {
        if (user.UserName != null && user.Email != null && user.Password != null) 
        {
            _usersRepository.CreateUser(user);
        }
        throw new BadRequestException("Данные введены не полностью");
    }

    public void DeleteUserById(Guid id)
    {
        var user=_usersRepository.GetUserById(id);
        if (user is null)
        {
            throw new NotFoundException($"Пользователь c id:{id} не найден");
        }
        _usersRepository.DeleteUser(user);
    }

    public List<UserDto> GetAllUsers()
    {
        var users = _usersRepository.GetAllUsers();
        if (users is null)
        {
            throw new NotFoundException($"Пользователи не найден");
        }
        return users;
        
    }

    public UserDto GetUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user is null)
        {
            throw new NotFoundException($"Пользователь c id:{id} не найден");
        }
        return user;
    }

    public void UpdateUser(Guid id, UserDto user)
    {
        var userFromDB = _usersRepository.GetUserById(id);
        if (userFromDB is null)
        {
            throw new NotFoundException($"Пользователь c id:{id} не найден");
        }
        _usersRepository.UpdateUser(userFromDB, user);
    }
}
