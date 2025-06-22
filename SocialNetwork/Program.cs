using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Views;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork
{
    class Program
    {
        static UserService userService;
        static IMessageService messageService; // Статическое поле для общего доступа

        public static MainView mainView;
        public static RegistrationView registrationView;
        public static AuthenticationView authenticationView;
        public static UserMenuView userMenuView;
        public static UserInfoView userInfoView;
        public static UserDataUpdateView userDataUpdateView;
        public static MessageSendingView messageSendingView;
        public static UserIncomingMessageView userIncomingMessageView;
        public static UserOutcomingMessageView userOutcomingMessageView;
        public static AddingFriendView addingFriendView;
        public static UserFriendView userFriendView;

        static void Main(string[] args)
        {
            // 1. Создаем экземпляры репозиториев
            IUserRepository userRepository = new UserRepository();
            IFriendRepository friendRepository = new FriendRepository();

            // 2. Создаем сервис сообщений ПЕРВЫМ
            messageService = new MessageService();

            // 3. Инициализируем UserService с зависимостями
            userService = new UserService(
                userRepository,
                friendRepository,
                messageService
            );

            // 4. Инициализация представлений с корректными зависимостями
            mainView = new MainView();
            registrationView = new RegistrationView(userService);
            authenticationView = new AuthenticationView(userService);
            userMenuView = new UserMenuView(userService);
            userInfoView = new UserInfoView();
            userDataUpdateView = new UserDataUpdateView(userService);
            messageSendingView = new MessageSendingView(messageService, userService);
            userIncomingMessageView = new UserIncomingMessageView();
            userOutcomingMessageView = new UserOutcomingMessageView();
            addingFriendView = new AddingFriendView(userService);
            userFriendView = new UserFriendView();

            while (true)
            {
                mainView.Show();
            }
        }
    }
}