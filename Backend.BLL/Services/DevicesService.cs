using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Backend.DAL.IRepositories;

namespace Backend.BLL.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;

    public DevicesService(IDevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }

    public void CreateDevice(DeviceDto device)
    {
        _devicesRepository.CreateDevice(device);
    }

    public void DeleteDevice(Guid id)
    {
        _devicesRepository.DeleteDevice(id);
    }

    public List<DeviceDto> GetAllDevices()
    {
        return _devicesRepository.GetAllDevices();
    }

    public DeviceDto GetDeviceById(Guid id)
    {
        return _devicesRepository.GetDeviceById(id);
    }

    public void UpdateDevice(Guid id, DeviceDto device)
    {
        _devicesRepository.UpdateDevice(id, device);
    }
}
