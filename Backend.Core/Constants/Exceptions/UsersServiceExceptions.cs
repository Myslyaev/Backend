namespace Backend.Core.Constants.Exceptions;

public static class UsersServiceExceptions
{
    public const string AuthenticationException = "Логин или пароль введены неверно";
    public const string NotFoundException = "Пользователь с Id {0} не найден";
}
