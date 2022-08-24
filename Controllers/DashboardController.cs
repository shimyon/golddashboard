using AngelOneAdmin.Models;
using AngelOneAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngelOneAdmin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        // GET: Angel
        AppDb _context = new AppDb();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDashboardData()
        {

            DashboardViewModel model = new DashboardViewModel();

            var LastNiftyIndex = _context.NiftyINdex.OrderByDescending(e => e.id).Select(e => new {e.close_price, e.index_time }).FirstOrDefault();
            model.NiftyIndexClosePrice = LastNiftyIndex.close_price;
            model.NiftyIndexTime = LastNiftyIndex.index_time.Date;
            model.NiftyTime = LastNiftyIndex.index_time.TimeOfDay.ToString();
            

            var LastBankNiftyIndex = _context.BankNiftyIndex.OrderByDescending(e => e.id).Select(e => new { e.close_price, e.index_time }).FirstOrDefault();
            model.BannkNiftyIndexClosePrice = LastBankNiftyIndex.close_price;
            model.BannkNiftyIndexTime = LastBankNiftyIndex.index_time.Date;
            model.BankNiftyTime = LastBankNiftyIndex.index_time.TimeOfDay.ToString();
      

            var CallBankNiftySp =_context.Database.SqlQuery<BankNifty>("call spGetBankNiftyIndex").Select(e => new BankNifty { indexName= e.indexName, price= e.price, createby=e.createby }).ToList();
            var CallTradingIndexSp = _context.Database.SqlQuery<Trading>("call spGetTradingIndex").Select(e => new Trading {indexName= e.indexName, price= e.price, createby=e.createby }).ToList();
            List<BankNifty> NiftyModel = new List<BankNifty>();
            foreach (var item in CallBankNiftySp)
            {
                BankNifty bankNiftyModel = new BankNifty();
                bankNiftyModel.indexName = item.indexName;
                bankNiftyModel.price = item.price;
                bankNiftyModel.createby = item.createby;
                NiftyModel.Add(bankNiftyModel);
            }

            List<Trading> TradingListModel = new List<Trading>();
            foreach (var item in CallTradingIndexSp)
            {
                Trading TradingModel = new Trading();
                TradingModel.indexName = item.indexName;
                TradingModel.price = item.price;
                TradingModel.createby = item.createby;
                TradingListModel.Add(TradingModel);
            }
            model.BankNifty = NiftyModel;
            model.Trading = TradingListModel;


            Session["NiftyIndexPrice"] = model.NiftyIndexClosePrice;
            Session["NiftyTime"] = model.NiftyTime;
            Session["BankNiftyIndexPrice"] = model.BannkNiftyIndexClosePrice;
            Session["BankNiftyTime"] = model.BankNiftyTime;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}