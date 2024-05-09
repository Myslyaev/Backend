using Backend.Core.Models.Users;

namespace Backend.BLL.IServices;

public interface IUsersService
{
    AuthenticatedResponse LoginUser(LoginUserRequest request);
    UserWithDevicesResponse GetUserById(Guid id);
    List<UserResponse> GetAllUsers();
    Guid CreateUser(CreateUserRequest request);
    Guid UpdateUser(UpdateUserRequest request);
    void DeleteUserById(Guid id);
}
