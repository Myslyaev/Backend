using Backend.Core.DTOs;

namespace Backend.BLL.IServices;

public interface IUsersService
{
    UserDto GetUserById(Guid id);
    List<UserDto> GetAllUsers();
}
