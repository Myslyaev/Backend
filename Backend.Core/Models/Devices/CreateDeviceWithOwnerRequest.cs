using Backend.Core.Enums;

namespace Backend.Core.Models.Devices;

public class CreateDeviceWithOwnerRequest
{
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public string Adress { get; set; }
    public Guid OwnerId { get; set; }
}
