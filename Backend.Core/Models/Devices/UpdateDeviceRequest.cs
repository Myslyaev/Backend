using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.Core.Models.Devices;

public class UpdateDeviceRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Adress { get; set; }
    public UserDto Owner { get; set; }
}