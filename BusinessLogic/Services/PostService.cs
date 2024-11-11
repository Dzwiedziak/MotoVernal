using BusinessLogic.DTO.Post;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Result<int?, PostErrorCode> Add(AddPostDTO post)
        {
            var newPost = CreateNewPost(post);
            _postRepository.Add(newPost);
            return newPost.Id;
        }

        public Result<GetPostDTO, PostErrorCode> Get(int id)
        {
            Post? dbPost = _postRepository.GetOne(id);
            if (dbPost is null)
                return PostErrorCode.PostNotFound;

            return CreateGetPostDTO(dbPost);
        }

        public PostErrorCode? Update(int id, UpdatePostDTO post)
        {
            Post? dbPost = _postRepository.GetOne(id);
            if (dbPost is null)
                return PostErrorCode.PostNotFound;

            UpdatePost(ref dbPost, post);
            _postRepository.Update(dbPost);
            return null;
        }

        private Post CreateNewPost(AddPostDTO post) =>
            new(post.Publisher, post.Content, publicationTime: DateTime.Now, post.Image);

        private GetPostDTO CreateGetPostDTO(Post post) =>
            new(post.Publisher, post.Content, post.PublicationTime, post.Image);

        private void UpdatePost(ref Post oldPost, UpdatePostDTO post)
        {
            oldPost.Content = post.Content;
            oldPost.Image = post.Image;
        }
    }
}
