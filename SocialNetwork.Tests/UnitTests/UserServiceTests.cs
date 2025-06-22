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
        var userService = new UserService();

        userService.SetUserRepository(mockUserRepo.Object);

        var userData = new UserRegistrationData
        {
            Email = "test@example.com",
            Password = "Password123",
            FirstName = "John",
            LastName = "Doe"
        };

        mockUserRepo
            .Setup(r => r.FindByEmail("test@example.com"))
            .Returns(new UserEntity());

        mockUserRepo.Object.FindByEmail("test@example.com");
        // пытается вызвать фрагмент кода, представленный как делегат, чтобы проверить, что он выдает определенное исключение
        var ex =  Assert.Throws<Exception>(() => userService.Register(userData));

        // Проверка сообщения
         Assert.AreEqual("A user with this email already exists.", ex.Message);
    }
}