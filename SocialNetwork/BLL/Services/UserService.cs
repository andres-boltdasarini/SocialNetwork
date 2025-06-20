using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        MessageService messageService;
        IUserRepository userRepository;
        //IFriendRepository friendRepository;
        FriendService friendService;

        public UserService()
        {
            userRepository = new UserRepository();
            //friendRepository = new FriendRepository();
            messageService = new MessageService();
            friendService = new FriendService();
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            if (string.IsNullOrEmpty(userRegistrationData.FirstName))
                throw new ArgumentNullException("Имя не может быть пустым");
            if (string.IsNullOrEmpty(userRegistrationData.LastName))
                throw new ArgumentNullException("Фамилия не может быть пустой");
            if (string.IsNullOrEmpty(userRegistrationData.Password))
                throw new ArgumentNullException("Пароль не может быть пустым");
            if (string.IsNullOrEmpty(userRegistrationData.Email))
                throw new ArgumentNullException("Email не может быть пустым");

            if (userRegistrationData.Password.Length < 8)
                throw new ArgumentException("Пароль должен содержать не менее 8 символов");

            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
                throw new ArgumentException("Некорректный формат email");

            if (userRepository.FindByEmail(userRegistrationData.Email) != null)
                throw new ArgumentException("Пользователь с таким email уже существует");

            var userEntity = new UserEntity()
            {
                firstname = userRegistrationData.FirstName,
                lastname = userRegistrationData.LastName,
                password = userRegistrationData.Password,
                email = userRegistrationData.Email
            };

            if (this.userRepository.Create(userEntity) == 0)
                throw new Exception("Ошибка при создании пользователя");
        }
        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }
        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook
            };

            if (this.userRepository.Update(updatableUserEntity) == 0)
                throw new Exception();
        }



        private User ConstructUserModel(UserEntity userEntity)
        {
            var incomingMessages = messageService.GetIncomingMessagesByUserId(userEntity.id);
            var outgoingMessages = messageService.GetOutcomingMessagesByUserId(userEntity.id);
            var friends = friendService.GetFriendsByUserId(userEntity.id);

            return new User(userEntity.id,
                          userEntity.firstname,
                          userEntity.lastname,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book,
                          incomingMessages,
                          outgoingMessages,
                          friends
                          );
        }
    }
}
