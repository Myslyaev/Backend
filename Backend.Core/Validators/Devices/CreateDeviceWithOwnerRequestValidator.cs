using Backend.Core.Models.Devices;
using FluentValidation;

namespace Backend.Core.Validators.Devices;

public class CreateDeviceWithOwnerRequestValidator : AbstractValidator<CreateDeviceWithOwnerRequest>
{
    public CreateDeviceWithOwnerRequestValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage("Введите название устройства");
        RuleFor(d => d.DeviceType).IsInEnum().WithMessage("Введите тип устройства");
        RuleFor(d => d.Adress).NotEmpty().WithMessage("Введите адрес расположения устройства");
        RuleFor(d => d.OwnerId).NotEmpty().WithMessage("Введите id владельца устройства");
    }
}

