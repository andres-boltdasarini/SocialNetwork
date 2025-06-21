using Moq;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

[TestFixture]
public class UserServiceTests
{
    [Test]
    public void Register_ExistingEmail_ThrowsException()
    {
        // Arrange
        var mockUserRepo = new Mock<IUserRepository>();
        var userService = new UserService(
            mockUserRepo.Object,
            Mock.Of<IFriendRepository>(),    // ���������� ���
            Mock.Of<IMessageService>()        // ���������� ���
        );

        var userData = new UserRegistrationData
        {
            Email = "test@example.com",
            Password = "Password123",
            FirstName = "John",
            LastName = "Doe"
        };

        // ���������: ��� ������ email ���������� ������������� ������������
        mockUserRepo
            .Setup(r => r.FindByEmail("test@example.com"))
            .Returns(new UserEntity()); // ��-null ������ = ������������ ����������

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => userService.Register(userData));

        // �������� ��������� ����������
        Assert.AreEqual("������������ � ����� email ��� ����������", ex.Message);
    }
}