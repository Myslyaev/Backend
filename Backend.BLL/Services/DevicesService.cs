using AutoMapper;
using Backend.BLL.IServices;
using Backend.Core.Constants.Exceptions;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Devices;
using Backend.DAL.IRepositories;
using FluentValidation;
using Serilog;
using ValidationException = Backend.Core.Exceptions.ValidationException;

namespace Backend.BLL.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<DevicesService>();
    private readonly IMapper _mapper;
    private readonly IValidator<CreateDeviceRequest> _deviceValidator;
    private readonly IValidator<CreateDeviceWithOwnerRequest> _deviceWithOwnerValidator;


    public DevicesService(IDevicesRepository devicesRepository, IUsersRepository usersRepository, IMapper mapper, IValidator<CreateDeviceRequest> deviceValidator,
                          IValidator<CreateDeviceWithOwnerRequest> deviceWithOwnerValidator)
    {
        _usersRepository = usersRepository;
        _devicesRepository = devicesRepository;
        _mapper = mapper;
        _deviceValidator = deviceValidator;
        _deviceWithOwnerValidator = deviceWithOwnerValidator;
    }

    public Guid CreateDevice(CreateDeviceRequest request)
    {
        var validationResult = _deviceValidator.Validate(request);
        if (validationResult.IsValid)
        {
            _logger.Information("Вызываем метод репозитория");
            var device = _mapper.Map<DeviceDto>(request);
            return _devicesRepository.CreateDevice(device);
        }
        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);

        throw new ValidationException(exceptions);
    }

    public void DeleteDeviceById(Guid id)
    {
        _logger.Information("Вызываем метод репозитория");
        var device = _devicesRepository.GetDeviceById(id);
        if (device is null)
        {
            _logger.Error($"Устройство c id:{id} не найдено");
            throw new NotFoundException(string.Format(DevicesServiceExceptions.NotFoundException, id));
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
            throw new NotFoundException(string.Format(DevicesServiceExceptions.NotFoundException, request.Id));
        }

        device.Name = request.Name;
        device.DeviceType = request.DeviceType;
        device.Adress = request.Adress;
        device.Owner = request.Owner;

        return _devicesRepository.UpdateDevice(device);
    }

    public Guid CreateDeviceWitnOwner(CreateDeviceWithOwnerRequest request)
    {
        var validationResult = _deviceWithOwnerValidator.Validate(request);
        if (validationResult.IsValid)
        {
            _logger.Information("Вызываем метод репозитория");
            var owner = _usersRepository.GetUserById(request.OwnerId);
            if (owner is null)
            {
                _logger.Error($"Пользователь c id:{request.OwnerId} не найден");
                throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, request.OwnerId));
            }

            var device = _mapper.Map<DeviceDto>(request);
            device.Owner = owner;
            return _devicesRepository.CreateDeviceWithOwner(device);
        }
        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);

        throw new ValidationException(exceptions);
    }
}
