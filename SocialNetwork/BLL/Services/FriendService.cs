using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        IUserRepository userRepository;
        IFriendRepository friendRepository;
        public FriendService()
        {
            userRepository = new UserRepository();
            friendRepository = new FriendRepository();
        }
        public IEnumerable<Friend> GetFriendsByUserId(int userId)
        {
            var friend = new List<Friend>();
            friendRepository.FindAllByUserId(userId).ToList().ForEach(f =>
            {
                var friendUserEntity = userRepository.FindById(f.user_id);

                friend.Add(new Friend(f.id, friendUserEntity.email));
            });
            return friend;
        }

        public void AddFriend(AddFriendData addFriendData)
        {
            var findUserEntity = this.userRepository.FindByEmail(addFriendData.FriendEmail); ////
            if (findUserEntity is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                user_id = addFriendData.UserId,
                friend_id = findUserEntity.id
            };
            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception("Ошибка при добавлении");
        }
    }
}
