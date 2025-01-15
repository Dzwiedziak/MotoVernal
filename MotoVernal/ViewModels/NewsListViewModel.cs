using BusinessLogic.DTO.Post;
using DB.Entities;

namespace MotoVernal.ViewModels
{
    public class NewsListViewModel
    {
        public List<GetPostDTO> PostsList { get; set; }
        public List<PostReaction> PostReactionsList { get; set; }
    }
}
