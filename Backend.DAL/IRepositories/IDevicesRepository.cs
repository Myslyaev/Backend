using Backend.Core.DTOs;

namespace Backend.DAL.IRepositories;

public interface IDevicesRepository
{
    Guid CreateDevice(DeviceDto device);
    void DeleteDevice(DeviceDto device);
    List<DeviceDto> GetAllDevices();
    DeviceDto GetDeviceById(Guid id);
    Guid UpdateDevice(DeviceDto device);
}