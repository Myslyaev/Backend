using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("/api/devices")]

public class DevicesController : Controller
{
    private readonly IDevicesService _devicesService;

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    [HttpGet]
    public List<DeviceDto> GetAllDevices()
    {
        return _devicesService.GetAllDevices();
    }

    [HttpGet("{id}")]
    public DeviceDto GetDeviceById(Guid id)
    {
        return _devicesService.GetDeviceById(id);
    }

    [HttpPost]
    public void CreateDevice([FromQuery] DeviceDto device)
    {
        _devicesService.CreateDevice(device);
    }

    [HttpPut("{id}")]
    public void UpdateDevice(Guid id, [FromQuery] DeviceDto device)
    {
        _devicesService.UpdateDevice(id, device);
    }

    [HttpDelete("{id}")]
    public void DeleteDevice(Guid id)
    {
        _devicesService.DeleteDevice(id);
    }
}
