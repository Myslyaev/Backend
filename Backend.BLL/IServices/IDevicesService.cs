using Backend.Core.DTOs;

namespace Backend.BLL.IServices
{
    public interface IDevicesService
    {
        void CreateDevice(DeviceDto device);
        void DeleteDevice(Guid id);
        List<DeviceDto> GetAllDevices();
        DeviceDto GetDeviceById(Guid id);
        void UpdateDevice(Guid id, DeviceDto device);
    }
}