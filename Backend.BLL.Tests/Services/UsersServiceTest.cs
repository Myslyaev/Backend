using AutoMapper;
using Backend.BLL.Services;
using Backend.Core.Constants.Exceptions;
using Backend.Core.DTOs;
using Backend.Core.Exceptions;
using Backend.Core.Models.Users;
using Backend.Core.Validators.Users;
using Backend.DAL.IRepositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using ValidationException = Backend.Core.Exceptions.ValidationException;

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
        public void CreateUserTest_ValidRequestSent_GuidReceived()
        {
            //arrange
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

        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("Test2")]
        [InlineData("Test")]
        public void CreateUserTest_RequestWithInvalidPasswordSent_PasswordErrorReceived(string password)
        {
            //arrange
            var expectedGuid = Guid.NewGuid();
            _usersRepositoryMock.Setup(r => r.CreateUser(It.IsAny<UserDto>())).Returns(expectedGuid);

            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);
            var invalidCreateUserRequest = new CreateUserRequest()
            {
                UserName = "Test",
                Password = password,
                Email = "test@ya.ru"
            };

            //act, assert
            Assert.Throws<ValidationException>(() => sut.CreateUser(invalidCreateUserRequest));
            _usersRepositoryMock.Verify(r => r.CreateUser(It.IsAny<UserDto>()), Times.Never);
        }

        [Fact]
        public void GetAllUsersTest_Called_UsersReceived()
        {
            //arrange
            var expected = new List<UserResponse>() { new UserResponse() { Email = "test@test.ru" }, { new UserResponse() { Email = "test@test.ru" } } };
            var expectedDto = new List<UserDto>() { new UserDto() { Email = "test@test.ru" }, { new UserDto() { Email = "test@test.ru" } } };
            _usersRepositoryMock.Setup(r => r.GetAllUsers()).Returns(expectedDto);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            var actual = sut.GetAllUsers();

            //assert
            actual.Should().BeEquivalentTo(expected);
            _usersRepositoryMock.Verify(r => r.GetAllUsers(), Times.Once);
        }

        [Fact]
        public void GetUserByIdTest_ValidIdSent_UserReceived()
        {
            //arrange
            Guid guid = new Guid();
            var expected = new UserResponse() { Email = "test@test.ru" };
            var expectedDto = new UserDto() { Email = "test@test.ru" };
            _usersRepositoryMock.Setup(r => r.GetUserById(It.IsAny<Guid>())).Returns(expectedDto);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            var actual = sut.GetUserById(guid);

            //assert
            actual.Should().BeEquivalentTo(expected);
            _usersRepositoryMock.Verify(r => r.GetUserById(guid), Times.Once);
        }

        [Fact]
        public void GetUserByIdTest_InValidIdSent_UserNotFoundErrorReceived()
        {
            //arrange
            Guid guid = new Guid();
            var expected = new UserResponse() { Email = "test@test.ru" };
            var expectedDto = new UserDto() { Email = "test@test.ru" };
            _usersRepositoryMock.Setup(r => r.GetUserById(guid)).Returns((UserDto)null);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            Action act = () => sut.GetUserById(guid);

            //assert
            act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, guid));
            _usersRepositoryMock.Verify(r => r.GetUserById(guid), Times.Once);
        }

        [Fact]
        public void GetUserByIdTest_EmptyIdSent_UserNotFoundErrorReceived()
        {
            //arrange
            Guid guid = Guid.Empty;
            var expected = new UserResponse() { Email = "test@test.ru" };
            var expectedDto = new UserDto() { Email = "test@test.ru" };
            _usersRepositoryMock.Setup(r => r.GetUserById(guid)).Returns((UserDto)null);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            Action act = () => sut.GetUserById(guid);

            //assert
            act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, guid));
            _usersRepositoryMock.Verify(r => r.GetUserById(guid), Times.Once);
        }

        [Fact]
        public void DeleteUserByIdTest_ValidIdSent_NoErrorsReceived()
        {
            //arrange
            Guid guid = new Guid();
            _usersRepositoryMock.Setup(r => r.GetUserById(guid)).Returns(new UserDto());
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            sut.DeleteUserById(guid);

            //assert
            _usersRepositoryMock.Verify(r => r.GetUserById(guid), Times.Once());
            _usersRepositoryMock.Verify(r => r.DeleteUser(It.IsAny<UserDto>()), Times.Once);
        }

        [Fact]
        public void DeleteUserByIdTest_EmptyIdSent_UserNotFoundErrorReceived()
        {
            //arrange
            Guid guid = Guid.Empty;
            _usersRepositoryMock.Setup(r => r.GetUserById(guid)).Returns((UserDto)null);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            Action act = () => sut.DeleteUserById(guid);

            //assert
            act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, guid));
            _usersRepositoryMock.Verify(r => r.GetUserById(guid), Times.Once());
            _usersRepositoryMock.Verify(r => r.DeleteUser(It.IsAny<UserDto>()), Times.Never);
        }

        [Fact]
        public void DeleteUserByIdTest_InValidIdSent_UserNotFoundErrorReceived()
        {
            //arrange
            Guid guid = new Guid();
            _usersRepositoryMock.Setup(r => r.GetUserById(guid)).Returns((UserDto)null);
            var sut = new UsersService(_usersRepositoryMock.Object, _mapper, _userValidator, _userUpdateValidator);

            //act
            Action act = () => sut.DeleteUserById(guid);

            //assert
            act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, guid));
            _usersRepositoryMock.Verify(r => r.GetUserById(guid), Times.Once());
            _usersRepositoryMock.Verify(r => r.DeleteUser(It.IsAny<UserDto>()), Times.Never);
        }


    }
}