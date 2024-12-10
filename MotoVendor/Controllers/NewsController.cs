using BusinessLogic.DTO.Post;
using BusinessLogic.DTO.PostComment;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Security.Policy;

namespace MotoVendor.Controllers
{
    public class NewsController : Controller
    {
        IPostService _postService;
        IUserService _userService;
        public NewsController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }
        /*
        public IActionResult PostsList()
        {
            var model = _postService.GetAll();
            return View(model);
        } */
        public IActionResult PostsList(string? relationStatus, string? creationTimeLTE, string? creationTimeGTE, string? search)
        {
            var posts = _postService.GetAll();

            if (!string.IsNullOrEmpty(relationStatus))
            {
                var currentUser = _userService.GetCurrentUser().Result;
                var currentUserId = currentUser?.Id; 
                if (relationStatus == "owner")
                {
                    posts = posts.Where(post => currentUserId != null && post.Publisher?.Id == currentUserId).ToList();
                }
                else if (relationStatus == "others")
                {
                    posts = posts.Where(post => currentUserId != null && post.Publisher?.Id != currentUserId).ToList();
                }
            }
            if (!string.IsNullOrEmpty(creationTimeLTE) && DateTime.TryParse(creationTimeLTE, out var lteDate))
            {
                posts = posts.Where(post => post.PublicationTime <= lteDate).ToList();
            }
            if (!string.IsNullOrEmpty(creationTimeGTE) && DateTime.TryParse(creationTimeGTE, out var gteDate))
            {
                posts = posts.Where(post => post.PublicationTime >= gteDate).ToList();
            }
            if (!string.IsNullOrEmpty(search))
            {
                posts = posts.Where(post => !string.IsNullOrEmpty(post.Content) && post.Content.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            posts = posts.OrderByDescending(post => post.PublicationTime).ToList();
            return View(posts);

        }
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPost(AddPostDTO post)
        {
            if(post.Content == null)
                post.Content = String.Empty;

            ModelState.Clear();
            var publisher = _userService.GetCurrentUser().Result;
            if(publisher != null)
            {
                post.Publisher = publisher;
                if(ModelState.IsValid)
                {
                    var result = _postService.Add(post);
                    if(result.IsSuccess)
                        return RedirectToAction("EditPost", new { id = result.Value });
                    return View("Error");
                }
                return View(post);
            }
            return RedirectToAction("AddPost");
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var result = _postService.Get(id);
            if(result.IsSuccess)
            {
                var updatePostDTO = CreateUpdatePostDTO(result.Value);
                return View(updatePostDTO);
            }
            return View("Error");
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditPost([FromRoute] int id, [FromForm] UpdatePostDTO post)
        {
            var errorCode = _postService.Update(id, post);
            if(errorCode == null)
            {
                var result = _postService.Update(id, post);
                if(result == null)
                    return RedirectToAction("EditPost", new { id = id });
                return View("Error");
            }
            return View("Error");
        }

        public IActionResult AddPostComment(int id, AddPostCommentDTO postComment)
        {
            _postService.AddPostComment(id, postComment);
            return RedirectToAction("PostsList");
        }

        public UpdatePostDTO CreateUpdatePostDTO(GetPostDTO post)
        {
            return new(post.Content, post.Image);
        }

    }
}
