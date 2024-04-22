using Backend.Core.DTOs;

namespace Backend.DAL.IRepositories;

public interface IUsersRepository
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetAllUsers();
    void CreateUser(UserDto user);
    void UpdateUser(UserDto userFromDB, UserDto user);
    void DeleteUser(UserDto user);
}
