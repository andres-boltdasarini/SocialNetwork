using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views
{
    public class AuthenticationView
    {
        UserService userService;
        public AuthenticationView(UserService userService)
        {
            this.userService = userService;
        }
        public void Show()
        {
            var authenticationData = new UserAuthenticationData();

            Console.WriteLine("Введите почтовый адрес:");
            authenticationData.Email = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            authenticationData.Password = Console.ReadLine();

            try
            {
                User user = this.userService.Authenticate(authenticationData);
                SuccessMessage.Show("Вы успешно вошли");
                SuccessMessage.Show("Добро пожаловать " + user.FirstName);

                Program.userMenuView.Show(user);
            }
            catch (WrongPasswordException ex)
            {
                AlertMessage.Show($"Пароль не корректный: {ex.Message}");
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show($"Пользователь не найден: {ex.Message}");
            }
        }
    }
}
