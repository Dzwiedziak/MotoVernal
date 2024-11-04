using BusinessLogic.DTO.Message;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Result<int?, MessageErrorCode> Add(SendMessageDTO message)
        {
            var newMessage = CreateNewMessage(message);
            _messageRepository.Add(newMessage);
            return newMessage.Id;
        }

        private Message CreateNewMessage(SendMessageDTO sendMessageDTO) =>
            new(sendMessageDTO.Broadcaster, sendMessageDTO.Reciever, sendMessageDTO.Content, DateTime.Now);
    }
}
