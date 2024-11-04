using BusinessLogic.DTO.Message;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IMessageService
    {
        Result<int?, MessageErrorCode> Add(SendMessageDTO message);
    }
}
