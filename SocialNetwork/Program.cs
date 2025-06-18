using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;

namespace SocialNetwork
{ 
    public class Program
    {
        public static UserService userService = new UserService();
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать");

            while (true)
            {

                Console.WriteLine("Для регистрации пользователя введите имя");
                string firstname = Console.ReadLine();
                Console.WriteLine("Фамилию");
                string lastname = Console.ReadLine();
                Console.WriteLine("Пароль");
                string password = Console.ReadLine();
                Console.WriteLine("Почта");
                string email = Console.ReadLine();

                UserRegistrationData userRegistrationData = new UserRegistrationData()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Password = password,
                    Email = email
                };

                try
                {
                    userService.Register(userRegistrationData);
                    Console.WriteLine("Регистрация прошла успешно");
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка при регистрации: {ex.Message}");
                }
                Console.ReadLine();
            }

        }
    }
}



