using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        IUserRepository userRepository;
        public UserService() {
        userRepository = new UserRepository();
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
    }
}
