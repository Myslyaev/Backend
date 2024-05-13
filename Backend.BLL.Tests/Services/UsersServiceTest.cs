using AutoMapper;
using Backend.BLL.Services;
using Backend.Core.DTOs;
using Backend.Core.Models.Users;
using Backend.Core.Validators.Users;
using Backend.DAL.IRepositories;
using FluentValidation;
using Moq;

namespace Backend.BLL.Tests.Services
{
    public class UsersServiceTest
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserRequest> _userValidator;
        private readonly IValidator<UpdateUserRequest> _userUpdateValidator;

        public UsersServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersMappingProfile());
            });

            _mapper = new Mapper(config);
            _userValidator = new CreateUserRequestValidator();
            _userUpdateValidator = new UpdateUserRequestValidator();
            _usersRepositoryMock = new Mock<IUsersRepository>();
        }

        [Fact]
        public void CreateUserTest_ValidRequestSend_GuidReceived()
        {
            //arange
            var validCreateUserRequest = new CreateUserRequest()
            {
                UserName = "Test",
                Password = "Test56test",
                Email = "test@ya.ru"
            };
            var expectedGuid = Guid.NewGuid();
            _usersRepositoryMock.Setup(r => r.CreateUser(It.IsAny<UserDto>())).Returns(expectedGuid);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            var actual = sut.CreateUser(validCreateUserRequest);

            //assert
            Assert.Equal(expectedGuid, actual);
            _usersRepositoryMock.Verify(r => r.CreateUser(It.IsAny<UserDto>()), Times.Once);
        }
    }
}