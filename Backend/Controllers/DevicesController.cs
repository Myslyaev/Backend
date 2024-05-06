using Backend.BLL.IServices;
using Backend.Core.Models.Devices;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.API.Controllers;

[ApiController]
[Route("/api/devices")]

public class DevicesController : Controller
{
    private readonly IDevicesService _devicesService;
    private readonly Serilog.ILogger _logger = Log.ForContext<DevicesController>();

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    [HttpGet]
    public ActionResult<List<DeviceResponse>> GetAllDevices()
    {
        _logger.Information("Получаем список всех устройств");
        return Ok(_devicesService.GetAllDevices());
    }

    [HttpGet("{id}")]
    public ActionResult<DeviceWithOwnerResponse> GetDeviceById(Guid id)
    {
        _logger.Information($"Получаем устройство по {id}");
        return Ok(_devicesService.GetDeviceById(id));
    }

    [HttpPost]
    public ActionResult<Guid> CreateDevice([FromBody] CreateDeviceRequest request)
    {
        _logger.Information($"Добавляем устройство {request.Name}");
        return Ok(_devicesService.CreateDevice(request));
    }

    [HttpPut("{id}")]
    public ActionResult UpdateDevice([FromBody] UpdateDeviceRequest request)
    {
        _logger.Information($"Изменяем инфорфацию об устройстве {request.Id}");
        return Ok(_devicesService.UpdateDevice(request));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteDeviceById(Guid id)
    {
        _logger.Information($"Удаляем устройство {id}");
        _devicesService.DeleteDeviceById(id);
        return Ok();
    }
}
