using Backend.Core.Models.Devices;

namespace Backend.BLL.IServices
{
    public interface IDevicesService
    {
        Guid CreateDevice(CreateDeviceRequest request);
        Guid CreateDeviceWitnOwner(CreateDeviceWithOwnerRequest request);
        void DeleteDeviceById(Guid id);
        List<DeviceResponse> GetAllDevices();
        DeviceWithOwnerResponse GetDeviceById(Guid id);
        Guid UpdateDevice(UpdateDeviceRequest request);
    }
}