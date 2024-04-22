using Backend.Core.DTOs;

namespace Backend.BLL.IServices;

public interface IUsersService
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetAllUsers();
    void CreateUser(UserDto user);
    void UpdateUser(Guid id, UserDto user);
    void DeleteUserById(Guid id);
}
