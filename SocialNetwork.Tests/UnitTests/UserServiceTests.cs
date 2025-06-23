using System.Reflection;
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
        var userService = new UserService(); // Создаем реальный экземпляр

        // Получаем поле userRepository через рефлексию
        var field = typeof(UserService).GetField(
            "userRepository",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
        // Подменяем репозиторий на мок
        field.SetValue(userService, mockUserRepo.Object);

        var userData = new UserRegistrationData
        {
            Email = "test@example.com",
            Password = "Password123",
            FirstName = "John",
            LastName = "Doe"
        };

        // Настраиваем мок: при вызове FindByEmail возвращаем сущность (как будто email уже занят)
        mockUserRepo
            .Setup(r => r.FindByEmail("test@example.com"))
            .Returns(new UserEntity());

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => userService.Register(userData));
        Assert.AreEqual("A user with this email already exists.", ex.Message);
    }
}