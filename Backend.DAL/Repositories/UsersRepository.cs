using Backend.Core.DTOs;
using Backend.DAL.IRepositories;

namespace Backend.DAL.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MamkinMainerContext context) : base(context)
    {
    }

    public List<UserDto> GetAllUsers()
    {
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id)
    {
        return new()
        {
            UserName = "TestName",
            Id = id,
            Password = "123",
            Email = "123@mail.ru"
        };
    }
}
