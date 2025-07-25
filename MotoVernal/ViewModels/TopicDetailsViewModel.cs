﻿using BusinessLogic.DTO.Topic;
using BusinessLogic.DTO.TopicResponse;

namespace MotoVernal.ViewModels
{
    public class TopicDetailsViewModel
    {
        public GetTopicDTO TopicInfo { get; set; }
        public List<GetTopicResponseDTO> Responses { get; set; }
        public AddTopicResponseDTO ResponseToAdd { get; set; }
    }
}
