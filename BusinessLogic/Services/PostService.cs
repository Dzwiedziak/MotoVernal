using BusinessLogic.DTO.EventInterest;
using BusinessLogic.DTO.Post;
using BusinessLogic.DTO.PostComment;
using BusinessLogic.DTO.PostReaction;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostReactionRepository _postReactionRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly IUserService _userService;
        public PostService(IPostRepository postRepository, IPostReactionRepository postReactionRepository, IPostCommentRepository postCommentRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _postReactionRepository = postReactionRepository;
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
        public Post? GetPost(int id)
        {
            return _postRepository.GetOne(id);
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
        public List<PostReaction> GetAllReactions()
        {
            return _postReactionRepository.GetAll();
        }
        public PostReactiontErrorCode? LikePost(PostReactionDTO postReaction)
        {
            PostReaction? dbpostReaction = _postReactionRepository.GetOne(postReaction.User.Id, postReaction.Post.Id);
            if (dbpostReaction is not null)
                return PostReactiontErrorCode.AlreadyLiked;

            _postReactionRepository.Add(CreatePostReaction(postReaction));
            return null;
        }

        public PostReactiontErrorCode? StopLikePost(PostReactionDTO postReaction)
        {
            PostReaction? dbpostReaction = _postReactionRepository.GetOne(postReaction.User.Id, postReaction.Post.Id);
            if (dbpostReaction is null)
                return PostReactiontErrorCode.AlreadyNotLiked;

            _postReactionRepository.Delete(dbpostReaction.Id);
            return null;
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
        public PostReaction CreatePostReaction(PostReactionDTO postReaction)
        {
            return new PostReaction(postReaction.User, postReaction.Post);
        }
    }
}
