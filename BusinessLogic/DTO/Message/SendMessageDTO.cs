using Entities = DB.Entities;

namespace BusinessLogic.DTO.Message
{
    public class SendMessageDTO
    {
        public Entities.User Broadcaster { get; set; }
        public Entities.User Reciever { get; set; }
        public string Content { get; set; }

        public SendMessageDTO(Entities.User broadcaster, Entities.User reciever, string content)
        {
            Broadcaster = broadcaster;
            Reciever = reciever;
            Content = content;
        }
    }
}
