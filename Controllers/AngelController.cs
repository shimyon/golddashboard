using AngelOneAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngelOneAdmin.Controllers
{
    public class AngelController : Controller
    {
        // GET: Angel
        AppDb _context = new AppDb();
        public ActionResult Index()
        {

            var data = _context.User.ToList();

            return View();
        }
    }
}