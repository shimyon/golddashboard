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
    public class ClientController : Controller
    {
        private AppDb _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        // GET: Account
        public ClientController()
        {

        }

        public ClientController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager, AppDb context)
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



        public ActionResult Index()
        {
            var getUserList = UserManager.Users.ToList();
            ViewBag.userList = getUserList;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetUserList()
        {
            var userContext = new AppDb();
            var userList = userContext.Users.ToList();

            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddUserData(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                string refId = "";
                if (model.ReferralPerson == "1")
                {
                    refId = null;
                }
                else {
                    refId = model.ReferralPerson;
                }
                var user = new AppUsers {GoldUserId =model.GoldUserId , Balance = model.Balance, Deposit= model.Deposit, Email = model.Email, UserName = model.Email, PhoneNumber = model.Phone, GoldUserName = model.GoldUserUserName, DepositDate = model.DepositDate, Percent = model.Percent, IsActive = model.isActive, ReferralPersonId =refId };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Json(new { success = true});
                }
                else 
                {
                    return Json(new { success = false});
                }
            } 
                
            
            return Json("Model state error");
        }

        [HttpGet]
        public ActionResult AddReferral()
        {
            List<referralListBoxVeiwModel> listBoxData = new List<referralListBoxVeiwModel>();
            var userContext = new AppDb();
            var userList = userContext.Users.Where(e =>  e.ReferralPersonId != null).Select(e => e.ReferralPersonId).Distinct().ToList();
            foreach (var item in userList)
            {
                referralListBoxVeiwModel listModel = new referralListBoxVeiwModel
                {
                    rUserId = item,
                    rUserName = userContext.Users.Where(e => e.Id == item).Select(e => e.GoldUserName).FirstOrDefault() + " - (" + userContext.Users.Where(e => e.Id == item).Select(e => e.Email).FirstOrDefault() + ")"
                };
                listBoxData.Add(listModel);
            }
            ViewBag.ReferralList = listBoxData;
            return View();
        }
        [HttpPost]
        public ActionResult AddReferralData(string referralId)
        {
            var userContext = new AppDb();
            var userList = userContext.Users.Where(e => e.ReferralPersonId == referralId && e.IsReferralAdded==false).ToList();

            return Json(userList);
        }

        public ActionResult GiveReferralMoney(GiveReferralMoneyViewModel giveMoneyModel)
        {
            var totalPercent = giveMoneyModel.Userpercent * giveMoneyModel.count;

            var userContext = new AppDb();
            var user = userContext.Users.Where(e => e.Id == giveMoneyModel.userId).FirstOrDefault();
            user.PerMonthProfit = user.PerMonthProfit + user.PerMonthProfit * totalPercent / 100;
            var count = userContext.SaveChanges();
            foreach (var item in giveMoneyModel.refferedUserId)
            {
                var refferedUser = userContext.Users.Where(e => e.Id == item).FirstOrDefault();
                refferedUser.IsReferralAdded = true;
                userContext.SaveChanges();

            }
            
            if (count == 1)
            {
                return Json(new { success = true });
            }
            else 
            {
                return Json(new { success = false });
            }
            
        }


    }
}