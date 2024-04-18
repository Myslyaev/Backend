using Backend.Core.Enums;

namespace Backend.Core.DTOs;

public class DeviceDto : IdContainer
{
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Adress {  get; set; }
    public UserDto Owner { get; set; }
}
