namespace Backend.DAL.Tests.Repositories;

using Backend.DAL.Repositories;
using MockQueryable.Moq;
using Moq;

public class UsersRepositoryTest
{
    private readonly Mock<MamkinMainerContext> _mamkinMainerContextMock;

    public UsersRepositoryTest()
    {
        _mamkinMainerContextMock = new Mock<MamkinMainerContext>();
    }

    [Fact]
    public void GetUserById_GuidSent_UserDtoWithDevicesReceieved()
    {
        //arrange
        var expected = new Guid("08006bdf-86de-492a-8b09-5b4833acadea");
        var mock = TestData.GetFakeUserDtoList().BuildMock().BuildMockDbSet();
        _mamkinMainerContextMock.Setup(x => x.Users).Returns(mock.Object);

        var sut = new UsersRepository(_mamkinMainerContextMock.Object);

        //act
        var actual = sut.GetUserById(expected);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Id);
    }
}