namespace SocialNetwork.BLL.Models
{
    public class Message
    {
        public int Id { get; }
        public string Content { get; set; }
        public int Sender_Id { get; }
        public int Recipient_Id { get; set; }


        public Message(
            int id,
            string content,
            int sender_id,
            int recipient_id)
        {
            this.Id = id;
            this.Content = content;
            this.Sender_Id = sender_id;
            this.Recipient_Id = recipient_id;
        }
    }
}