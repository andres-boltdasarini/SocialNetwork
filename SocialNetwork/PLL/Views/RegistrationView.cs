using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;

namespace SocialNetwork.PLL.Views
{
    public class RegistrationView
    {
        UserService userService;
        public RegistrationView(UserService userService)
        {
            this.userService = userService;
        }
        public void Show()
        {
            var userRegistrationData = new UserRegistrationData();

            Console.WriteLine("Для создания нового профиля введите ваше имя:");
            userRegistrationData.FirstName = Console.ReadLine();

            Console.Write("Ваша фамилия:");
            userRegistrationData.LastName = Console.ReadLine();

            Console.Write("Пароль:");
            userRegistrationData.Password = Console.ReadLine();

            Console.Write("Почтовый адрес:");
            userRegistrationData.Email = Console.ReadLine();

            try
            {
                userService.Register(userRegistrationData);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ваш профиль успешно создан. Теперь Вы можете войти в систему под своими учетными данными.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Введите корректное значение.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при регистрации: {ex.Message}");
            }
        }
    }
}
