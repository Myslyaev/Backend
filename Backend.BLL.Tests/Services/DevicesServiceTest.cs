using AutoMapper;
using Backend.BLL.Services;
using Backend.Core.DTOs;
using Backend.Core.Enums;
using Backend.Core.Models.Devices;
using Backend.Core.Models.Users;
using Backend.Core.Validators.Devices;
using Backend.Core.Validators.Users;
using Backend.DAL.IRepositories;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Tests.Services
{
    public class DevicesServiceTest
    {
        private readonly Mock<IDevicesRepository> _devicesRepositoryMock;
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDeviceRequest> _deviceValidator;
        private readonly IValidator<CreateDeviceWithOwnerRequest> _deviceWithOwnerValidator;

        public DevicesServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersMappingProfile());
                cfg.AddProfile(new DevicesMappingProfile());
            });

            _mapper = new Mapper(config);
            _deviceValidator = new CreateDeviceRequestValidator();
            _deviceWithOwnerValidator = new CreateDeviceWithOwnerRequestValidator();
            _devicesRepositoryMock = new Mock<IDevicesRepository>();
            _usersRepositoryMock = new Mock<IUsersRepository>();
        }

        [Fact]
        public void CreateDeviceTest_ValidRequestSent_GuidReceived()
        {
            //arange
            var validCreateDeviceRequest = new CreateDeviceRequest()
            {
                Name ="Test",
                DeviceType=DeviceType.PC,
                Adress="TestTest"
            };
            var expectedGuid = Guid.NewGuid();
            _devicesRepositoryMock.Setup(r => r.CreateDevice(It.IsAny<DeviceDto>())).Returns(expectedGuid);
            var sut = new DevicesService(_devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper, _deviceValidator, _deviceWithOwnerValidator);

            //act
            var actual = sut.CreateDevice(validCreateDeviceRequest);

            //assert
            Assert.Equal(expectedGuid, actual);
            _devicesRepositoryMock.Verify(r => r.CreateDevice(It.IsAny<DeviceDto>()), Times.Once);
        }
    }
}
