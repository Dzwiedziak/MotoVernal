using BusinessLogic.DTO.Post;
using DB.Entities;

namespace MotoVendor.ViewModels
{
    public class NewsListViewModel
    {
        public List<GetPostDTO> PostsList { get; set; }
        public List<PostReaction> PostReactionsList { get; set; }
    }
}
