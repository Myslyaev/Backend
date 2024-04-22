using Backend.Core.DTOs;
using Backend.DAL.IRepositories;

namespace Backend.DAL.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    public DevicesRepository(MamkinMainerContext context) : base(context)
    {
    }

    public List<DeviceDto> GetAllDevices() => _ctx.Devices.ToList();

    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);

    public void CreateDevice(DeviceDto device) => _ctx.Devices.Add(device);

    public void DeleteDevice(Guid id)
    {
        var device = GetDeviceById(id);
        _ctx.Devices.Remove(device);
    }

    public void UpdateDevice(Guid id, DeviceDto device)
    {
        var devicewFromDb = GetDeviceById(id);

        if (devicewFromDb != null)
        {
            devicewFromDb.DeviceType = device.DeviceType;
            devicewFromDb.Name = device.Name;
            devicewFromDb.Adress = device.Adress;
            devicewFromDb.Owner = device.Owner;

            _ctx.Devices.Update(devicewFromDb);
        }
    }
}
