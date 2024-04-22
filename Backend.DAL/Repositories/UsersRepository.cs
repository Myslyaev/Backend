using Backend.Core.DTOs;
using Backend.DAL.IRepositories;

namespace Backend.DAL.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MamkinMainerContext context) : base(context)
    {
    }

    public List<UserDto> GetAllUsers()=> _ctx.Users.ToList();
  
    public UserDto GetUserById(Guid id) => _ctx.Users.FirstOrDefault(u => u.Id == id);

    public void CreateUser(UserDto user) => _ctx.Users.Add(user);

    public void DeleteUser(UserDto user) => _ctx.Users.Remove(user);
       
    public void UpdateUser(UserDto userFromDb, UserDto user)
    {
            userFromDb.UserName = user.UserName;
            userFromDb.Password = user.Password;
            userFromDb.Devices = user.Devices;
            userFromDb.Email = user.Email;

            _ctx.Users.Update(userFromDb);
    }
}
