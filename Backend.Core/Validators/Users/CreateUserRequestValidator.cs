using Backend.Core.Models.Users;
using FluentValidation;

namespace Backend.Core.Validators.Users;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(u => u.UserName).NotEmpty().NotNull().WithMessage("Введите имя пользователя");
        RuleFor(u => u.Email).EmailAddress().WithMessage("Почта введена в неверном формате");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Введите пароль")
            .MinimumLength(8).WithMessage("Пароль должен быть длиной не менее 8 символов")
            .Matches(@"[A-Z]+").WithMessage("Пароль должен содержать хотя бы 1 заглавную букву")
            .Matches(@"[a-z]+").WithMessage("Пароль должен содержать хотя бы 1 строчную букву.")
            .Matches(@"[0-9]+").WithMessage("Пароль должен содержать хотя бы 1 цифру");
    }
}
