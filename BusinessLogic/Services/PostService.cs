using BusinessLogic.DTO.Post;
using BusinessLogic.DTO.PostComment;
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
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly IUserService _userService;
        public PostService(IPostRepository postRepository, IPostCommentRepository postCommentRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _userService = userService;
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

        public PostErrorCode? Update( UpdatePostDTO post)
        {
            Post? dbPost = _postRepository.GetOne(post.Id);
            if (dbPost is null)
                return PostErrorCode.PostNotFound;

            UpdatePost(ref dbPost, post);
            _postRepository.Update(dbPost);
            return null;
        }

        private Post CreateNewPost(AddPostDTO post) =>
            new(post.Publisher, post.Content, publicationTime: DateTime.Now, post.Image);

        private GetPostDTO CreateGetPostDTO(Post post) =>
            new(post.Id, post.Publisher, post.Content, post.PublicationTime, post.Image, post.PostComments);

        private void UpdatePost(ref Post oldPost, UpdatePostDTO post)
        {
            oldPost.Content = post.Content;
            oldPost.Image = post.Image;
        }

        public List<GetPostDTO> GetAll()
        {
            return _postRepository.GetAll().Select(p => CreateGetPostDTO(p)).ToList();
        }

        public PostCommentErrorCode? AddPostComment(int id, AddPostCommentDTO postComment)
        {
            PostComment? addingPostComment = CreatePostComment(id, postComment);
            if (addingPostComment is null)
                return PostCommentErrorCode.PostDoesNotExist;
            _postCommentRepository.Add(addingPostComment);
            return null;
        }
        private PostComment? CreatePostComment(int id, AddPostCommentDTO postComment)
        {
            User? publisher = _userService.GetCurrentUser().Result;
            if (publisher is null)
                return null;
            Post? post = _postRepository.GetOne(id);
            if (post == null)
                return null;
            return new PostComment(0, postComment.Content, post, publisher, DateTime.Now, new List<PostCommentReaction>());
        }
        public PostCommentErrorCode? UpdatePostComment(int postCommentId, UpdatePostCommentDTO postComment)
        {
            PostComment dbPostComment = _postCommentRepository.Get(postCommentId);
            if (dbPostComment is null)
            {
                return PostCommentErrorCode.PostCommentDoesNotExist;
            }
            UpdatePostComment(ref dbPostComment, postComment);
            _postCommentRepository.Update(dbPostComment);
            return null;
        }
        private void UpdatePostComment(ref PostComment dbPostComment, UpdatePostCommentDTO updatedPostComment)
        {
            dbPostComment.Content = updatedPostComment.Content;
        }
    }
}
