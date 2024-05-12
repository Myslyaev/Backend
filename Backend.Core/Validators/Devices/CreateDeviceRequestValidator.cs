using Backend.Core.Models.Devices;
using FluentValidation;

namespace Backend.Core.Validators.Devices;

public class CreateDeviceRequestValidator : AbstractValidator<CreateDeviceRequest>
{
    public CreateDeviceRequestValidator()
    {
        RuleFor(d => d.Name).NotEmpty().NotNull().WithMessage("Введите название устройства");
        RuleFor(d => d.DeviceType).IsInEnum().WithMessage("Введите тип устройства");
        RuleFor(d => d.Adress).NotEmpty().NotNull().WithMessage("Введите адрес расположения устройства");
    }
}

