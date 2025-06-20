namespace SocialNetwork.BLL.Models
{
    public class Friend2
    {
        public int Id { get; set; }
        public string FriendEmail { get; set; }

        public Friend2(int id, string friendEmail)
        {
            Id = id;
            FriendEmail = friendEmail;
        }
    }

}
