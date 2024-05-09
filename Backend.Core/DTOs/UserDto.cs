namespace Backend.Core.DTOs;

public class UserDto : IdContainer
{
    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }

    public string Email { get; set; }

    public List<DeviceDto> Devices { get; set; }
}
