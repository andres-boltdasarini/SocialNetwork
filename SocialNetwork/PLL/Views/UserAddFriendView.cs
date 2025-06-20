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
    public class UserAddFriendView
    {
        UserService userService;
        FriendService friendService;

        public UserAddFriendView(FriendService friendService, UserService userService)
        {
            this.friendService = friendService;
            this.userService = userService;
        }

        public void Show(UserDataUpdateView user)
        {
            var addFriendData = new AddFriendData();

            Console.Write("Ведите почтовый адрес друга: ");
            addFriendData.FriendEmail = Console.ReadLine();

            try
            {
                friendService.AddFriend(addFriendData);
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден");
            }
            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение");
            }
            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при добавлении");
            }
        }
    }
}
