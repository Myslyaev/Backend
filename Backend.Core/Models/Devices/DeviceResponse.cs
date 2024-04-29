using Backend.Core.Enums;

namespace Backend.Core.Models.Devices;

public class DeviceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Adress { get; set; }
}
