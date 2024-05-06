using AutoMapper;
using Backend.BLL.IServices;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Devices;
using Backend.DAL.IRepositories;
using Serilog;

namespace Backend.BLL.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<DevicesService>();
    private readonly IMapper _mapper;


    public DevicesService(IDevicesRepository devicesRepository, IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _devicesRepository = devicesRepository;
        _mapper = mapper;
    }

    public Guid CreateDevice(CreateDeviceRequest request)
    {
        _logger.Information("Вызываем метод репозитория");
        var device = _mapper.Map<DeviceDto>(request);
        return _devicesRepository.CreateDevice(device);
    }

    public void DeleteDeviceById(Guid id)
    {
        _logger.Information("Вызываем метод репозитория");
        var device = _devicesRepository.GetDeviceById(id);
        if (device is null)
        {
            _logger.Error($"Устройство c id:{id} не найдено");
            throw new NotFoundException($"Устройство c id:{id} не найдено");
        }
        _devicesRepository.DeleteDevice(device);
    }

    public List<DeviceResponse> GetAllDevices()
    {
        _logger.Information("Вызываем метод репозитория");
        List<DeviceDto> deviceDtos = _devicesRepository.GetAllDevices();
        return _mapper.Map<List<DeviceResponse>>(deviceDtos);
    }

    public DeviceWithOwnerResponse GetDeviceById(Guid id)
    {
        _logger.Information("Вызываем метод репозитория");
        DeviceDto deviceDto = _devicesRepository.GetDeviceById(id);
        return _mapper.Map<DeviceWithOwnerResponse>(deviceDto);
    }

    public Guid UpdateDevice(UpdateDeviceRequest request)
    {
        _logger.Information("Вызываем метод репозитория");
        var device = _devicesRepository.GetDeviceById(request.Id);
        if (device is null)
        {
            _logger.Error($"Устройство c id:{request.Id} не найдено");
            throw new NotFoundException($"Устройство c id:{request.Id} не найдено");
        }

        device.Name = request.Name;
        device.DeviceType = request.DeviceType;
        device.Adress = request.Adress;
        device.Owner = request.Owner;

        return _devicesRepository.UpdateDevice(device);
    }

    public Guid CreateDeviceWitnOwner(CreateDeviceWithOwnerRequest request)
    {
        _logger.Information("Вызываем метод репозитория");
        var owner = _usersRepository.GetUserById(request.OwnerId);
        if (owner is null)
        {
            _logger.Error($"Пользователь c id:{request.OwnerId} не найден");
            throw new NotFoundException($"Пользователь c id:{request.OwnerId} не найден");
        }

        var device = _mapper.Map<DeviceDto>(request);
        device.Owner = owner;
        return _devicesRepository.CreateDeviceWithOwner(device);
    }
}
