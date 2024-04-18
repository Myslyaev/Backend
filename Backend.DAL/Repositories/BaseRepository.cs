namespace Backend.DAL.Repositories;

public class BaseRepository
{
    protected readonly MamkinMainerContext _ctx;

    public BaseRepository(MamkinMainerContext context)
    {
        _ctx = context;
    }
}
