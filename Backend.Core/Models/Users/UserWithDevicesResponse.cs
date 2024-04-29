using Backend.Core.Models.Devices;

namespace Backend.Core.Models.Users;

public class UserWithDevicesResponse : UserResponse
{
    public List<DeviceResponse> Devices { get; set; }
}
