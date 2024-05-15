using Backend.Core.Enums;

namespace Backend.Core.Models.Devices;

public class CreateDeviceRequest
{
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public string Adress { get; set; }
}
