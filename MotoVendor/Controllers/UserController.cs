using BusinessLogic.DTO.Ban;
using BusinessLogic.DTO.Report;
using BusinessLogic.DTO.User;
using BusinessLogic.DTO.UserObservation;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.Security.Claims;


namespace MotoVendor.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBanService _banService;
        private readonly IReportService _reportService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, IBanService banService, IReportService reportService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userService = userService;
            _banService = banService;
            _reportService = reportService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var referer = Request.Headers["Referer"].ToString();
            TempData["AuthState"] = "Register";

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserDTO user)
        {
            var referer = Request.Headers["Referer"].ToString();
            var errors = new Dictionary<string, List<string>>();
            TempData["AuthState"] = "None";

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(user.UserName);
                if (existingUser != null)
                {
                    errors["UserName"] = new List<string> { "User with this nickname already exists" };
                    TempData["AuthState"] = "Register";
                }
                else
                {
                    var existingEmail = await _userManager.FindByEmailAsync(user.Email);
                    if (existingEmail != null)
                    {
                        errors["Email"] = new List<string> { "User with this email already exists" };
                        TempData["AuthState"] = "Register";
                    }
                    else
                    {
                        var newUser = new User(user.UserName, user.Email, null, null, DateTime.Now, "");

                        var result = await _userManager.CreateAsync(newUser, user.Password);
                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(referer))
                            {
                                return Redirect(referer);
                            }

                            return RedirectToAction("IntroductionPage", "Home");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                errors["General"] = errors.GetValueOrDefault("General", new List<string>());
                                errors["General"].Add(error.Description);
                                TempData["AuthState"] = "Register";
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var fieldErrors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToList();
                    if (fieldErrors.Any())
                    {
                        errors[key] = fieldErrors;
                    }
                }
                TempData["AuthState"] = "Register";
            }

            TempData["RegisterModel"] = JsonConvert.SerializeObject(user);
            TempData["RegisterErrors"] = JsonConvert.SerializeObject(errors);

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var referer = Request.Headers["Referer"].ToString();
            TempData["AuthState"] = "Login";

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            TempData["AuthState"] = "None";
            var referer = Request.Headers["Referer"].ToString();
            var errors = new Dictionary<string, List<string>>();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(referer))
                        {
                            return Redirect(referer);
                        }
                        return RedirectToAction("IntroductionPage", "Home");
                    }

                    errors["UserName"] = new List<string> { "Invalid login attempt" };
                    TempData["AuthState"] = "Login";
                }
                else
                {
                    errors["UserName"] = new List<string> { "User not found" };
                    TempData["AuthState"] = "Login";
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var fieldErrors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToList();
                    if (fieldErrors.Any())
                    {
                        errors[key] = fieldErrors;
                    }
                }
                TempData["AuthState"] = "Login";
            }
            TempData["LoginModel"] = JsonConvert.SerializeObject(model);
            TempData["LoginErrors"] = JsonConvert.SerializeObject(errors);

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            var referer = Request.Headers["Referer"].ToString();
            await _signInManager.SignOutAsync();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return RedirectToAction("IntroductionPage", "Home");
        }

        [HttpGet]
        public IActionResult ProfilesList(string search, string sortBy, bool? observed, bool? admin)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<GetUserDTO> dbUsers = _userService.GetAll(currentUserId);
            if (observed.HasValue && observed.Value)
                dbUsers = dbUsers.Where(u => u.IsObserved).ToList();
            if (admin.HasValue && admin.Value)
                dbUsers = dbUsers.Where(u => u.Roles.Contains("Admin")).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                dbUsers = dbUsers.Where(u => u.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (sortBy)
            {
                case "alphabetical_asc":
                    dbUsers = dbUsers.OrderBy(u => u.UserName).ToList();
                    break;
                case "alphabetical_desc":
                    dbUsers = dbUsers.OrderByDescending(u => u.UserName).ToList();
                    break;
                default:
                    dbUsers = dbUsers.OrderBy(u => u.UserName).ToList();
                    break;
            }
            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            ViewBag.Observed = observed;
            ViewBag.Admin = admin;
            return View(dbUsers);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("IntroductionPage", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult ProfileView(string id)
        {
            var result = _userService.Get(id);

            if (result.Error == UserErrorCode.UserNotFound)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction("Error", "Home");
            }
            var currentUser = User.Identity.Name;
            var isCurrentUser = result.Value.UserName == currentUser;
            bool isAdmin = User.IsInRole("Admin");

            var activeBan = _banService.GetActiveBan(id);

            if (activeBan != null)
            {
                ViewBag.BanId = activeBan.Id;
                var isBanned = activeBan != null;
                ViewBag.IsBanned = isBanned;
            }
            else
            {
                ViewBag.IsBanned = false;
            }
            ViewBag.IsCurrentUser = isCurrentUser;
            ViewBag.IsAdmin = isAdmin;
            ViewBag.BanExpiration = activeBan?.ExpirationTime;

            return View(result.Value);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditProfile(string id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != id)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this profile.";
                return RedirectToAction("Error", "Home");
            }
            var result = _userService.Get(id);
            if (result.Error == UserErrorCode.UserNotFound)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction("Error", "Home");
            }
            var updateUserDTO = new UpdateUserDTO(
                id,
                result.Value.Gender,
                result.Value.Age,
                result.Value.Description,
                result.Value.ProfileImage
            );

            return View(updateUserDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(UpdateUserDTO updateUserDTO)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != updateUserDTO.Id)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this profile.";
                return RedirectToAction("Error", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(updateUserDTO);
            }

            _userService.Update(updateUserDTO.Id, updateUserDTO);
            return RedirectToAction("ProfileView", new { id = updateUserDTO.Id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> BanAccount(string id)
        {
            var bannedUser = _userService.GetUser(id);
            var bannerUser = await _userManager.GetUserAsync(User);

            var result = _banService.GetActiveBan(id);
            if (result != null)
            {
                TempData["ErrorMessage"] = "This user is aldready banned.";
                return RedirectToAction("Error", "Home");
            }

            if (bannedUser == bannerUser)
            {
                TempData["ErrorMessage"] = "You can't ban yourself.";
                return RedirectToAction("Error", "Home");
            }

            var isBannedUserAdmin = await _userManager.IsInRoleAsync(bannedUser, "Admin");
            if (isBannedUserAdmin)
            {
                TempData["ErrorMessage"] = "You cannot ban another admin.";
                return RedirectToAction("Error", "Home");
            }

            var model = new BanUserDTO
            {
                Banned = bannedUser,
                Banner = bannerUser,
                ExpirationTime = DateTime.Today.AddDays(1),
                Reason = string.Empty,
                Image = null
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> BanAccount(BanUserDTO model)
        {

            var bannedUser = _userService.GetUser(model.Banned.Id);
            var bannerUser = await _userManager.GetUserAsync(User);

            var isBannedUserAdmin = await _userManager.IsInRoleAsync(bannedUser, "Admin");
            if (isBannedUserAdmin)
            {
                TempData["ErrorMessage"] = "You cannot ban another admin.";
                return RedirectToAction("Error", "Home");
            }
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            model.Banned = bannedUser;
            model.Banner = bannerUser;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _banService.BanUser(model);
            if (result.Error == BanErrorCode.UserAlreadyBanned)
            {
                TempData["ErrorMessage"] = "User is already banned";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("ProfileView", new { id = model.Banned.Id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveBan(string id)
        {
            var activeBan = _banService.GetActiveBan(id);

            if (activeBan == null)
            {
                TempData["ErrorMessage"] = "No active ban found for the user.";
                return RedirectToAction("Error", "Home");
            }

            ViewBag.UserId = id;
            ViewBag.UserName = activeBan.Banned.UserName;
            ViewBag.BanExpiration = activeBan.ExpirationTime;

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveBanConfirmed(string id)
        {
            var result = _banService.UnbanUser(id);
            if (result.Error == BanErrorCode.NoActiveBan)
            {
                TempData["ErrorMessage"] = "No active ban found for the user.";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("ProfileView", new { id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult BansList(string sortBy, string search, bool? active, bool? unactive)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _banService.GetAllBans();
            if (active.HasValue && active.Value)
                list = list.Where(b => b.ExpirationTime >= DateTime.Now).ToList();
            if (unactive.HasValue && unactive.Value)
                list = list.Where(b => b.ExpirationTime <= DateTime.Now).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(b => b.Banned.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (sortBy)
            {
                case "creation_time_asc":
                    list = list.OrderBy(b => b.CreationTime).ToList();
                    break;
                case "creation_time_desc":
                    list = list.OrderByDescending(b => b.CreationTime).ToList();
                    break;
                case "expiration_time_asc":
                    list = list.OrderBy(b => b.ExpirationTime).ToList();
                    break;
                case "expiration_time_desc":
                    list = list.OrderByDescending(b => b.ExpirationTime).ToList();
                    break;
                default:
                    list = list.OrderBy(b => b.CreationTime).ToList();
                    break;
            }
            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            ViewBag.Active = active;
            ViewBag.Unactive = unactive;
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public IActionResult BanDetails(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ban = _banService.GetBanById(id);
            if (ban == null)
            {
                TempData["ErrorMessage"] = "No ban found for this Id.";
                return RedirectToAction("Error", "Home");
            }
            if (!(User.IsInRole("Admin") || currentUserId == ban.Banned.Id))
            {
                TempData["ErrorMessage"] = "You are not authorized to see this ban.";
                return RedirectToAction("Error", "Home");
            }
            bool isActiveBan = ban.ExpirationTime > DateTime.Now;
            ViewBag.isActive = isActiveBan;
            return View(ban);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ReportAccount(string id)
        {
            var reportedUser = _userService.GetUser(id);
            var reporterUser = await _userManager.GetUserAsync(User);

            if (reportedUser == reporterUser)
            {
                TempData["ErrorMessage"] = "You can't report yourself.";
                return RedirectToAction("Error", "Home");
            }

            var model = new ReportUserDTO
            {
                Reporter = reporterUser,
                Reported = reportedUser,
                Description = string.Empty,
                Image = null
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportAccount(ReportUserDTO model)
        {
            var reportedUser = _userService.GetUser(model.Reported.Id);
            var reporterUser = await _userManager.GetUserAsync(User);

            model.Reporter = reporterUser;
            model.Reported = reportedUser;
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _reportService.ReportUser(model);
            return RedirectToAction("reportsList");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ReportsList(string sortBy, string search)
        {
            if (User.IsInRole("Admin"))
            {
                var list = _reportService.GetAllReports();
                if (!string.IsNullOrEmpty(search))
                {
                    list = list.Where(r => r.Reporter.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                switch (sortBy)
                {
                    case "creation_time_asc":
                        list = list.OrderBy(r => r.CreationTime).ToList();
                        break;
                    case "creation_time_desc":
                        list = list.OrderByDescending(r => r.CreationTime).ToList();
                        break;   
                    default:
                        list = list.OrderBy(r => r.CreationTime).ToList();
                        break;
                }
                ViewBag.Search = search;
                ViewBag.SortBy = sortBy;
                return View(list);
            }
            else
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var list = _reportService.GetReporterReports(currentUserId);
                if (!string.IsNullOrEmpty(search))
                {
                    list = list.Where(r => r.Reporter.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                switch (sortBy)
                {
                    case "creation_time_asc":
                        list = list.OrderBy(r => r.CreationTime).ToList();
                        break;
                    case "creation_time_desc":
                        list = list.OrderByDescending(r => r.CreationTime).ToList();
                        break;
                    default:
                        list = list.OrderBy(r => r.CreationTime).ToList();
                        break;
                }
                ViewBag.Search = search;
                ViewBag.SortBy = sortBy;
                return View(list);
            } 
        }
        [Authorize]
        [HttpGet]
        public IActionResult ReportDetails(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var report = _reportService.GetReportById(id);
            if (report == null)
            {
                TempData["ErrorMessage"] = "No report found for this Id.";
                return RedirectToAction("Error", "Home");
            }
            if (!(User.IsInRole("Admin") || currentUserId == report.Reporter.Id))
            {
                TempData["ErrorMessage"] = "You are not authorized to see this report.";
                return RedirectToAction("Error", "Home");
            }

            return View(report);
        }
        [HttpGet]
        [Authorize]
        public IActionResult RejectReport(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var report = _reportService.GetReportById(id);

            ViewBag.currentUserId = currentUserId;

            if (!(User.IsInRole("Admin") || currentUserId == report.Reporter.Id))
            {
                TempData["ErrorMessage"] = "You are not authorized to reject this report.";
                return RedirectToAction("Error", "Home");
            }

            if (report == null)
            {
                TempData["ErrorMessage"] = "No report found for this Id.";
                return RedirectToAction("Error", "Home");
            }

            return View(report);
        }
        [HttpPost]
        [Authorize]
        public IActionResult RejectReportConfirmed(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var report = _reportService.GetReportById(id);

            if (!(User.IsInRole("Admin") || currentUserId == report.Reporter.Id))
            {
                TempData["ErrorMessage"] = "You are not authorized to reject this report.";
                return RedirectToAction("Error", "Home");
            }
            if (report == null)
            {
                TempData["ErrorMessage"] = "No report found for this Id.";
                return RedirectToAction("Error", "Home");
            }
            _reportService.RejectReport(id);
            return RedirectToAction("ReportsList");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ObserveUser(string id)
        {
            var observedUser = _userService.GetUser(id);
            var observerUser = await _userManager.GetUserAsync(User);

            if (observerUser == observedUser)
            {
                TempData["ErrorMessage"] = "You cannot follow yourself.";
                return RedirectToAction("Error", "Home");
            }
            var model = new ObserveUserDTO
            {
                Observer = observerUser,
                Observed = observedUser
            };
            var result = _userService.ObserveUser(model);
            if (result.Value != null)
            {
                return RedirectToAction("ProfilesList");
            }
            else if (result.Error == UserObservationErrorCode.UserAlreadyFollowing)
            {
                TempData["ErrorMessage"] = "This user is aldready following by you.";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("ProfilesList");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> StopObserveUser(string id)
        {
            var observedUser = _userService.GetUser(id);
            var observerUser = await _userManager.GetUserAsync(User);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (observerUser == observedUser)
            {
                TempData["ErrorMessage"] = "You cannot unfollow yourself.";
                return RedirectToAction("Error", "Home");
            }

            var result = _userService.StopObservingUser(currentUserId, id);
            if (result == UserObservationErrorCode.UserObservationNotFound)
            {
                TempData["ErrorMessage"] = "You are not observing this user.";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("ProfilesList");
        }
    }
}
