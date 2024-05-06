using Backend.Core.DTOs;
using Backend.DAL.IRepositories;
using Serilog;

namespace Backend.DAL.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    private readonly ILogger _logger = Log.ForContext<DevicesRepository>();

    public DevicesRepository(MamkinMainerContext context) : base(context)
    {
    }

    public List<DeviceDto> GetAllDevices()
    {
        _logger.Information("Запрашиваем список устройств из базы");
        return _ctx.Devices.ToList();
    }

    public DeviceDto GetDeviceById(Guid id)
    {
        _logger.Information($"Ищем в базе устройство {id}");
        return _ctx.Devices.FirstOrDefault(d => d.Id == id);
    }

    public Guid CreateDeviceWithOwner(DeviceDto device)
    {
        _logger.Information($"Добавляем устройство {device.Name} принадлежащее пользователю {device.Owner.Id} в базу данных");
        _ctx.Devices.Add(device);
        _logger.Information($"Устройство добавлено. Возвращаем Id устройства:{device.Name}");
        return device.Id;
    }
    public Guid CreateDevice(DeviceDto device)
    {
        _logger.Information($"Добавляем устройство {device.Name} в базу данных");
        _ctx.Devices.Add(device);
        _ctx.SaveChanges();

        _logger.Information($"Устройство добавлено. Возвращаем Id устройства:{device.Name}");
        return device.Id;
    }

    public void DeleteDevice(DeviceDto device)
    {
        _logger.Information($"Удаляем устройство из базы данных: {device.Name}");
        _ctx.Devices.Remove(device);
        _ctx.SaveChanges();
    }

    public Guid UpdateDevice(DeviceDto device)
    {
        _logger.Information($"Обновляем информацию об устройстве в базе данных: {device.Id}");
        _ctx.Devices.Update(device);
        _ctx.SaveChanges();

        _logger.Information($"Информация обновлена. Возвращаем Id устройства: {device.Id}");
        return device.Id;
    }
}
