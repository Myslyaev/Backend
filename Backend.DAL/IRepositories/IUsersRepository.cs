using Backend.Core.DTOs;

namespace Backend.DAL.IRepositories;

public interface IUsersRepository
{
    UserDto GetUserById(Guid id);
    UserDto GetUserByMail(string mail);
    List<UserDto> GetAllUsers();
    Guid CreateUser(UserDto user);
    Guid UpdateUser(UserDto user);
    void DeleteUser(UserDto user);
}
