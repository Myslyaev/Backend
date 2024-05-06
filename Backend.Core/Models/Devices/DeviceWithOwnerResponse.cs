using Backend.Core.Enums;
using Backend.Core.Models.Users;

namespace Backend.Core.Models.Devices;

public class DeviceWithOwnerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Adress { get; set; }
    public UserResponse Owner { get; set; }
}
