using GoldDashboard;
using GoldDashboard.Models;
using GoldDashboard.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoldDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private AppDb _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        // GET: Account
        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager, AppDb context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AppRoleManager = roleManager;
            _context = context;
        }

        public ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }




        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUsers {UserName=model.Email, Email = model.Email, GoldUserName=model.GoldUserUserName, GoldUserId=model.GoldUserId, Balance=model.Balance, Deposit=model.Deposit };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddRole(AddRoleViewModel model)
        {


            //AppDb context = new AppDb();
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await AppRoleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {


                    return Json("Added");
                }
            }

            return View(model);
        }

        public ActionResult GetRoleList()
        {
            var roleList = AppRoleManager.Roles.ToList();
            return Json(roleList, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DeleteRole(string roleId)
        {
            var role = await AppRoleManager.FindByIdAsync(roleId);
           var isDeleted = await AppRoleManager.DeleteAsync(role);
            if (isDeleted.Succeeded == true)
            {
                return Json("Deleted");
            }
            else {
                return Json("NotDeleted");
            }

            
        }

        public async Task<ActionResult> AddUserToRole(AddUserToRoleViewModel addUserList)
        {
            if (addUserList.UserList != null)
            {
                foreach (var item in addUserList.UserList)
                {
                    var user = await UserManager.FindByNameAsync(item);
                    var roleResult = await UserManager.AddToRoleAsync(user.Id, addUserList.RoleName);
                }
            }

            if (addUserList.RemoveUserFromRoleList != null)
            {
                foreach (var item in addUserList.RemoveUserFromRoleList)
                {
                    var user = await UserManager.FindByNameAsync(item);
                    var roleResult = await UserManager.RemoveFromRoleAsync(user.Id, addUserList.RoleName);
                }
            }

            return Json("Added");
        }

        public ActionResult GetUserList(string name)
        {
            List<UserInRoleViewModel> isInRoleList = new List<UserInRoleViewModel>();
            var userList = UserManager.Users.ToList();
            foreach (var item in userList)
            {
                UserInRoleViewModel inRoleList = new UserInRoleViewModel();
                inRoleList.Email = item.Email;
                var check = UserManager.IsInRole(item.Id, name);
                inRoleList.IsInRole = check;
                isInRoleList.Add(inRoleList);

            }
            return Json(isInRoleList, JsonRequestBehavior.AllowGet);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    //var user = UserManager.FindByEmail(model.Email);
                    //var isInAdmin = UserManager.IsInRole(user.Id, "Admin");
                    //if (isInAdmin == true)
                    //{
                    //    return RedirectToLocal(returnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToLocalForUser(returnUrl, "User");
                    //}
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
          
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public void Authorize()
        {
    
        }
        [HttpGet]
        public ActionResult AddData()
        {
            //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            //var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            //var userId = claim.Value;

            //ViewBag.CurrentUserId = userId;
            var userContext = new AppDb();
            var users =  userContext.Users.ToList();
            return View(users);
        }
        [HttpPost]
        public async Task<ActionResult> AddData(AddDataViewModel model)
        {
            string id = model.UserId;
            var usertoupdate = await UserManager.FindByIdAsync(id);
            usertoupdate.Balance = model.Balance;
            usertoupdate.Deposit = model.Deposite;
            usertoupdate.PerDayProfit = model.PerDayProfit;
            usertoupdate.PerWeekProfit = model.PerWeekProfit;
            usertoupdate.PerMonthProfit = model.PerMonthProfit;

            var result = await UserManager.UpdateAsync(usertoupdate);

            //if (result.Succeeded)
            //{
            //    ViewBag.Message = "Data Update Successfully";
            //    return RedirectToAction("AddData", "Account");
                
            //}
            if (result.Succeeded)
            {
                return Json("Updated");

            }




            return View();
        }


        public async Task<ActionResult> getSingleUserData(string userId)
        {
            string id = userId;
            var userContext = new AppDb();
            var currentUser = userContext.Users.Where(u => u.Id == userId).FirstOrDefault(); 

            if (currentUser != null)
            {
                return Json(currentUser);
            }
            else {
                return Json("User Not Fount");
            }
        }
    }
}