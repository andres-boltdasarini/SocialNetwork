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
            Mock.Of<IFriendRepository>(),    // Упрощённый мок
            Mock.Of<IMessageService>()        // Упрощённый мок
        );

        var userData = new UserRegistrationData
        {
            Email = "test@example.com",
            Password = "Password123",
            FirstName = "John",
            LastName = "Doe"
        };

        // Настройка: при поиске email возвращаем существующего пользователя
        mockUserRepo
            .Setup(r => r.FindByEmail("test@example.com"))
            .Returns(new UserEntity()); // Не-null объект = пользователь существует

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => userService.Register(userData));

        // Проверка сообщения исключения
        Assert.AreEqual("Пользователь с таким email уже существует", ex.Message);
    }
}