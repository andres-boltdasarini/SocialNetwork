using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views
{
    public class MessageSendingView
    {
        UserService userService;
        MessageService messageService;
        public MessageSendingView(MessageService messageService, UserService userService) 
        {
            this.messageService = messageService;
            this.userService = userService;
        }
        public void Show(User user)
        {
            var messageSendingData = new MessageSendingData();

            Console.WriteLine();
            Console.Write("Ведите почтовый адрес получателя: ");
            messageSendingData.RecipientEmail = Console.ReadLine();

            Console.Write("Ведите Сообщение: ");
            messageSendingData.Content = Console.ReadLine();

            messageSendingData.SenderId = user.Id;

            try
            {
                messageService.SendMessage(messageSendingData);

                SuccessMessage.Show("Сообщение успешно отправлено");

                user = userService.FindById(user.Id);///////
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
                AlertMessage.Show("Произошла ошибка при отправке");
            }
        }
    }
}
