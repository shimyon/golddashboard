using AngelOneAdmin.Models;
using AngelOneAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngelOneAdmin.Controllers
{
    public class TradingController : Controller
    {

        AppDb _context = new AppDb();
        // GET: Trading
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ManualTradingViewModel TradingModel)
        {
            if (ModelState.IsValid)
            {
                
            }

            ManualTrading model = new ManualTrading();
            using (var context = new AppDb())
            {
                var users = _context.Database.SqlQuery<UserVewModel>("select userId, angel_user from users where active='Y'");
                foreach (var item in users)
                {
                    
                    model.tradingSymbol = TradingModel.IndexName;
                    model.price = TradingModel.Price;
                    model.lotsize = TradingModel.LotSize;
                    model.addedDate = DateTime.Now;
                    model.IsExecute = 0;
                    model.optionType  = TradingModel.IndexName.Substring((TradingModel.IndexName.Length)-2, 2);
                    model.transactionType = TradingModel.TrasactionType;
                    model.clientId = item.angel_user;
                }
            }
            
            _context.ManualTrading.Add(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public ActionResult GetManualTradingList()
        {
            var manualTradingEntries = _context.ManualTrading.OrderByDescending(e => e.addedDate).Take(10).ToList();

            return View(manualTradingEntries);
        }

        public ActionResult GetIndexNameAutoSuggestion(string keyword)
        {
            var loadedValue = _context.TradingIndex.Where(e => e.indexName.StartsWith(keyword)).Select(e => e.indexName).ToList();

            return Json(loadedValue);
        }

        public ActionResult GetFullIndexName(string keyword)
        {
            var loadedValue = _context.TradingIndex.Where(e => e.indexName==keyword).Select(e => e.indexName).ToList();

            return Json(loadedValue);
        }


        public ActionResult GetIndexList(string searchTerm)
        {

            var loadedValue = _context.TradingIndex.Where(e => e.indexName.Contains(searchTerm)).Take(10).Select(e => new {id = e.id, text = e.indexName }).ToList();
            return Json(loadedValue, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTradeBook(string search)
        {

            var model = _context.Database.SqlQuery<TradeBookViewModel>("call spGetTradeBook").Where(e => e.buyQty!= e.sellQty ).Select(e => new TradeBookViewModel { id=e.id, clientId=e.clientId, tradingSymbol=e.tradingSymbol, buyQty=e.buyQty, sellQty=e.sellQty, currentPrice=e.currentPrice, stopLoss=e.stopLoss}).ToList();

            return View(model);
        }

        public ActionResult AddTradeBookRecord(TradeBookViewModel TradeBookModel)
        {
            var getSingleRecord = _context.TradeBook.Where(e => e.id == TradeBookModel.id).FirstOrDefault();

            TradeBook AddTradeBook = new TradeBook {
                
                orderId = TradeBookModel.orderId,
                clientId = TradeBookModel.clientId,
                tradingSymbol = TradeBookModel.tradingSymbol,
                optionType = TradeBookModel.tradingSymbol.Substring((TradeBookModel.tradingSymbol.Length) - 2, 2),
                transactionType= TradeBookModel.TransationType,
                price=TradeBookModel.Price,
                quantity= TradeBookModel.Quantity,
                addedDate=DateTime.Now
            };
            _context.TradeBook.Add(AddTradeBook);
            _context.SaveChanges();
            return Json(AddTradeBook);
        }
        public ActionResult UserList()
        {
            var model = _context.User.ToList();
            return View(model);
        }
        public ActionResult AddUserDetail(AddUserViewModel AddUserModel)
        {

            User AddUser = new User
            {
                userId =AddUserModel.userId,
                password =AddUserModel.password,
                email = AddUserModel.email,
                Phone = AddUserModel.Phone,
                angel_user= AddUserModel.angel_user,
                angel_password =AddUserModel.angel_password,
                balance_amount = 0,
                apikey="",
                active=AddUserModel.active,
                addDate=DateTime.Now
            };

            _context.User.Add(AddUser);
            var isAdded = _context.SaveChanges();
            return Json(isAdded);
        }
    }
}
