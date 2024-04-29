using Backend.Core.Models.Devices;

namespace Backend.BLL.IServices
{
    public interface IDevicesService
    {
        Guid CreateDevice(CreateDeviceRequest request);
        void DeleteDeviceById(Guid id);
        List<DeviceResponse> GetAllDevices();
        DeviceResponse GetDeviceById(Guid id);
        Guid UpdateDevice(UpdateDeviceRequest request);
    }
}