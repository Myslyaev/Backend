using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Backend.DAL.IRepositories;

namespace Backend.BLL.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public List<UserDto> GetAllUsers()
    {
        return _usersRepository.GetAllUsers();
    }

    public UserDto GetUserById(Guid id)
    {
        return _usersRepository.GetUserById(id);
    }
}
