using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.EventInterest;
using BusinessLogic.DTO.Post;
using BusinessLogic.DTO.PostComment;
using BusinessLogic.DTO.PostCommentReaction;
using BusinessLogic.DTO.PostReaction;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using DB.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotoVernal.ViewModels;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotoVernal.Controllers
{
    public class NewsController : Controller
    {
        IPostService _postService;
        IUserService _userService;
        IBanService _banService;
        IPostCommentReactionService _postCommentReactionService;
        IPostCommentService _postCommentService;
        public NewsController(IPostService postService, IUserService userService, IBanService banService, IPostCommentReactionService postCommentReactionService, IPostCommentService postCommentService)
        {
            _postService = postService;
            _userService = userService;
            _banService = banService;
            _postCommentReactionService = postCommentReactionService;
            _postCommentService = postCommentService;
        }
        [Authorize]
        public IActionResult PostsList(string sortBy, bool? owner, bool? liked, bool? member, DateTime? dateFrom, DateTime? dateTo, string? search)
        {
            var posts = _postService.GetAll();
            var postsReactions = _postService.GetAllReactions();
            var currentUser = _userService.GetCurrentUser().Result;
            var currentUserId = currentUser?.Id;

            if (owner.HasValue && owner.Value || member.HasValue && member.Value || liked.HasValue && liked.Value)
            {
                posts = posts.Where(post =>
                    currentUserId != null &&
                    (
                        owner.HasValue && owner.Value && post.Publisher?.Id == currentUserId ||
                        member.HasValue && member.Value && post.PostComments.Any(comment => comment.Publisher.Id == currentUserId) ||
                        liked.HasValue && liked.Value && postsReactions.Any(reaction => reaction.Post.Id == post.Id && reaction.User.Id == currentUserId)
                    )
                ).ToList();
            }

            if (dateFrom.HasValue)
                posts = posts.Where(e => e.PublicationTime >= dateFrom.Value).ToList();
            if (dateTo.HasValue)
                posts = posts.Where(e => e.PublicationTime <= dateTo.Value).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                posts = posts.Where(post =>
                    !string.IsNullOrEmpty(post.Content) && post.Content.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    post.Publisher.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            switch (sortBy)
            {
                case "date_asc":
                    posts = posts.OrderBy(p => p.PublicationTime).ToList();
                    break;
                case "date_desc":
                    posts = posts.OrderByDescending(p => p.PublicationTime).ToList();
                    break;
                default:
                    posts = posts.OrderBy(p => p.PublicationTime).ToList();
                    break;
            }
            ViewBag.DateFrom = dateFrom?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.DateTo = dateTo?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.SortBy = sortBy;
            ViewBag.Search = search;
            ViewBag.Owner = owner;
            ViewBag.Liked = liked;
            ViewBag.Member = member;
            var vm = new NewsListViewModel
            {
                PostsList = posts,
                PostReactionsList = postsReactions
            };
            return View(vm);

        }
        [Authorize]
        [HttpGet]
        public IActionResult AddPost()
        {
            var currentUser = _userService.GetCurrentUser().Result;

            var result = _banService.GetActiveBan(currentUser.Id);
            if (result != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually add new post.";
                return RedirectToAction("Error", "Home");
            }
            var model = new AddPostDTO
            {
                Publisher = currentUser,
                Content = string.Empty,
                Image = null
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPost(AddPostDTO model)
        {
            var currentUser = _userService.GetCurrentUser().Result;
            model.Publisher = currentUser;
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
            _postService.Add(model);
            return RedirectToAction("PostsList");
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var user = _userService.GetCurrentUser().Result;
            var resultBan = _banService.GetActiveBan(user.Id);
            if (resultBan != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot edit your post.";
                return RedirectToAction("Error", "Home");
            }
            var result = _postService.Get(id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Post of this ID not exist";
                return RedirectToAction("Error", "Home");
            }
            else if (user.Id != result.Value.Publisher.Id)
            {
                TempData["ErrorMessage"] = "You are not a owner of this post";
                return RedirectToAction("Error", "Home");
            }
            var updatePostDTO = CreateUpdatePostDTO(result.Value);
            return View(updatePostDTO);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditPost(UpdatePostDTO post)
        {
            if (post.Image?.Base64 == "defaultBase64Value" && post.Image?.Extension == "defaultExtension")
            {
                post.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            var currentUser = _userService.GetCurrentUser().Result;
            var resultBan = _banService.GetActiveBan(currentUser.Id);
            if (resultBan != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot edit your post.";
                return RedirectToAction("Error", "Home");
            }
            var result = _postService.Get(post.Id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Post not exist";
                return RedirectToAction("Error", "Home");
            }
            else if (currentUser.Id != result.Value.Publisher.Id)
            {
                TempData["ErrorMessage"] = "You are not a owner of this post";
                return RedirectToAction("Error", "Home");
            }
            _postService.Update(post);
            return RedirectToAction("PostsList");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPostComment(int id, AddPostCommentDTO postComment)
        {
            var user = _userService.GetCurrentUser().Result;
            var resultBan = _banService.GetActiveBan(user.Id);
            if (resultBan != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot add new comment.";
                return RedirectToAction("Error", "Home");
            }
            _postService.AddPostComment(id, postComment);
            return RedirectToAction("PostsList");
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpdatePostComment(int id, UpdatePostCommentDTO postComment)
        {
            var user = _userService.GetCurrentUser().Result;

            var result = _postCommentService.Get(id);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Post not exist";
                return RedirectToAction("Error", "Home");
            }
            else if (user.Id != result.Publisher.Id)
            {
                TempData["ErrorMessage"] = "You are not a owner of this comment";
                return RedirectToAction("Error", "Home");
            }
            var resultBan = _banService.GetActiveBan(user.Id);
            if (resultBan != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot edit your comment.";
                return RedirectToAction("Error", "Home");
            }
            _postService.UpdatePostComment(id, postComment);
            return RedirectToAction("PostsList");
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteComment(int commentId)
        {
            var currentUser = _userService.GetCurrentUser().Result;
            var comment = _postCommentService.Get(commentId);

            if (comment == null)
            {
                TempData["ErrorMessage"] = "Comment not found.";
                return RedirectToAction("Error", "Home");
            }

            if (comment.Publisher.Id != currentUser.Id && !User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "You are not authorized to delete this comment.";
                return RedirectToAction("Error", "Home");
            }

            _postCommentService.Delete(commentId);

            return RedirectToAction("PostsList");
        }
        [HttpPost]
        [Authorize]
        public IActionResult LikePost(int id)
        {
            var currentUser = _userService.GetCurrentUser().Result;
            var result = _postService.Get(id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Post not exist";
                return RedirectToAction("Error", "Home");
            }
            var currentPost = _postService.GetPost(id);
            var model = new PostReactionDTO
            {
                User = currentUser,
                Post = currentPost
            };
            var likeResult = _postService.LikePost(model);
            if (likeResult == null)
            {
                return RedirectToAction("PostsList");
            }
            else if (likeResult == PostReactiontErrorCode.AlreadyLiked)
            {
                TempData["ErrorMessage"] = "You already liked this post.";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("PostsList");
        }
        [HttpPost]
        [Authorize]
        public IActionResult RemoveLikePost(int id)
        {
            var currentUser = _userService.GetCurrentUser().Result;
            var result = _postService.Get(id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Post not exist";
                return RedirectToAction("Error", "Home");
            }
            var currentPost = _postService.GetPost(id);
            var model = new PostReactionDTO
            {
                User = currentUser,
                Post = currentPost
            };
            var likeResult = _postService.StopLikePost(model);
            if (likeResult == null)
            {
                return RedirectToAction("PostsList");
            }
            else if (likeResult == PostReactiontErrorCode.AlreadyNotLiked)
            {
                TempData["ErrorMessage"] = "You already remove like from this post.";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("PostsList");
        }
        [Authorize]
        public IActionResult PostCommentLike(int postCommentId)
        {
            User user = _userService.GetCurrentUser().Result;
            var postCommentReaction = CreatePostCommentReaction(user, postCommentId, ReactionType.Like);
            _postCommentReactionService.AddOrUpdate(postCommentReaction);
            return RedirectToAction("PostsList");
        }
        [Authorize]
        public IActionResult PostCommentDislike(int postCommentId)
        {
            User user = _userService.GetCurrentUser().Result;
            var postCommentReaction = CreatePostCommentReaction(user, postCommentId, ReactionType.Dislike);
            _postCommentReactionService.AddOrUpdate(postCommentReaction);
            return RedirectToAction("PostsList");
        }

        public PostCommentReactionDTO CreatePostCommentReaction(User user, int postCommentId, ReactionType reactionType)
        {
            PostComment? postComment = _postCommentService.Get(postCommentId);
            if (postComment == null)
                return null;
            return new PostCommentReactionDTO(reactionType, user, postComment);
        }
        public UpdatePostDTO CreateUpdatePostDTO(GetPostDTO post)
        {
            return new(post.Id, post.Content, post.Image);
        }

    }
}
