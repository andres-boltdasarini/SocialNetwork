﻿using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views
{
    public class UserMenuView
    {
        UserService userService;
        public UserMenuView(UserService userService) 
        {
            this.userService = userService;
        }
        public void Show(User user)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Входящие сообщения: {0}", user.IncomingMessages.Count());
                Console.WriteLine("Исходящие сообщения: {0}", user.OutcomingMessages.Count());

                Console.WriteLine("Просмотреть информацию о моём профиле (нажмите 1)");
                Console.WriteLine("Редактировать мой профиль (нажмите 2)");
                Console.WriteLine("Добавить в друзья (нажмите 3)");
                Console.WriteLine("Написать сообщение (нажмите 4)");
                Console.WriteLine("Посмотреть входящие сообщения (нажмите 5)");
                Console.WriteLine("Посмотреть исходящие сообщения (нажмите 6)");
                Console.WriteLine("Просмотреть друзей (нажмите 7)");
                Console.WriteLine("Выйти из профиля (нажмите 8)");

                string keyValue = Console.ReadLine();

                if (keyValue == "8") break;

                switch (keyValue)
                {
                    case "1":
                        {
                            Program.userInfoView.Show(user);
                            break;
                        }
                    case "2":
                        {
                            Program.userDataUpdateView.Show(user);
                            break;
                        }
                    case "3":
                        {
                            Program.addingFriendView.Show(user);
                            user = userService.FindById(user.Id);
                            break;
                        }

                    case "4":
                        {
                            Program.messageSendingView.Show(user);
                            user = userService.FindById(user.Id);
                            break;
                        }
                    case "5":
                        {

                            Program.userIncomingMessageView.Show(user.IncomingMessages);
                            break;
                        }

                    case "6":
                        {
                            Program.userOutcomingMessageView.Show(user.OutcomingMessages);
                            break;
                        }

                    case "7":
                        {
                            Program.userFriendView.Show(user.Friends);
                            break;
                        }
                }   
            }
        }    
    }
}
