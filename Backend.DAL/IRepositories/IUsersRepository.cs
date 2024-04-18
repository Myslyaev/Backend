using Backend.Core.DTOs;

namespace Backend.DAL.IRepositories;

public interface IUsersRepository
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetAllUsers();
}
