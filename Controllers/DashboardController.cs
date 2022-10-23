using GoldDashboard.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoldDashboard.Controllers
{
    //[RequireHttps]
    [Authorize]
    public class DashboardController : Controller
    {
        private ApplicationUserManager _userManager;
        // GET: Dashboard
        public DashboardController()
        {

        }
        public DashboardController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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


        public async Task<ActionResult> Index()
        {
            AppDb context = new AppDb();
            var currentRate = context.setting.FirstOrDefault();
            AppUsers userid = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            userid.USD_Rate = currentRate.CurrencyPrice;
            return View(userid);
        }
    }
}