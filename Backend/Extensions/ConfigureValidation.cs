using Backend.Core.Validators.Devices;
using Backend.Core.Validators.Users;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Backend.API.Extensions;

public static class ValidationExtensions
{
    public static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>()
                .AddValidatorsFromAssemblyContaining<UpdateUserRequestValidator>()
                .AddValidatorsFromAssemblyContaining<CreateDeviceRequestValidator>()
                .AddValidatorsFromAssemblyContaining<CreateDeviceWithOwnerRequestValidator>();
    }
}
