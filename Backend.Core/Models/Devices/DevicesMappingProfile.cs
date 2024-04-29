using AutoMapper;
using Backend.Core.DTOs;

namespace Backend.Core.Models.Devices;

public class DevicesMappingProfile : Profile
{
    public DevicesMappingProfile()
    {
        CreateMap<DeviceDto, DeviceResponse>();

        CreateMap<CreateDeviceRequest, DeviceDto>();

        CreateMap<UpdateDeviceRequest, DeviceDto>();
    }
}
