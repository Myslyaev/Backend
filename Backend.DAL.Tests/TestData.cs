using Backend.Core.DTOs;
using Backend.Core.Enums;

namespace Backend.DAL.Tests;

public static class TestData
{
    public static List<UserDto> GetFakeUserDtoList() =>
        [
        new()
        {
            Id = new Guid("08006bdf-86de-492a-8b09-5b4833acadea"),
            UserName = "User",
            Email = "test@test.ru",
            Devices = [
                        new()
                        {
                            Id = new Guid("3ae2c668-caf2-4600-bbb2-250f5b378753"),
                            Name = "TestDevice1",
                            Type = DeviceType.VideoCard,
                            Adress = "TestTest"
                        },
                        new()
                        {
                            Id = new Guid("dd2d94cb-b313-4cda-a0a6-bf8906857aad"),
                            Name = "TestDevice2",
                            Type = DeviceType.PC,
                            Adress = "TestTestTest"
                        },
                      ],
        }
        ];
}
