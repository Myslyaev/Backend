using Backend.Core.DTOs;
using Backend.DAL.IRepositories;
using Serilog;

namespace Backend.DAL.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public UsersRepository(MamkinMainerContext context) : base(context)
    {
    }

    public List<UserDto> GetAllUsers()
    {
        _logger.Information("Запрашиваем список пользователей из базы");
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information($"Ищем в базе пользователя {id}");
        return _ctx.Users.FirstOrDefault(u => u.Id == id);
    }

    public Guid CreateUser(UserDto user)
    {
        _logger.Information($"Добавляем прльзователя {user.Email} в базу данных");
        _ctx.Users.Add(user);
        _ctx.SaveChanges();

        _logger.Information($"Позователь добавлен. Возвращаем Id пользователя:{user.Email}");
        return user.Id;
    }

    public void DeleteUser(UserDto user)
    {
        _logger.Information($"Удаляем пользователя из базы данных: {user.Email}");
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
    }

    public Guid UpdateUser(UserDto user)
    {
        _logger.Information($"Обновляем информацию о пользователя в базе данных: {user.Id}");
        _ctx.Users.Update(user);
        _ctx.SaveChanges();

        _logger.Information($"Информация обновлена. Возвращаем Id пользователя: {user.Id}");
        return user.Id;
    }
}
