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
            string selectedUserName = "";
            if (TradingModel.SelectedUser != null) 
            {
                foreach (var item in TradingModel.SelectedUser)
            {
                selectedUserName = selectedUserName + "," + item;
            }
            selectedUserName = selectedUserName.Remove(0, 1);
            }
            

            ManualTrading model = new ManualTrading();
            using (var context = new AppDb())
            {
                   
                    model.tradingSymbol = TradingModel.IndexName;
                    model.symbolToken = TradingModel.TokenSymbol;
                    model.price = TradingModel.Price;
                    model.lotsize = TradingModel.LotSize;
                    model.addedDate = DateTime.Now;
                    model.IsExecute = 0;
                    model.optionType  = TradingModel.IndexName.Substring((TradingModel.IndexName.Length)-2, 2);
                    model.transactionType = TradingModel.TrasactionType;
                    model.selectedUser = selectedUserName;
            }
            _context.ManualTrading.Add(model);
            var isAdded = _context.SaveChanges();
            return Json(isAdded);
        }


        public ActionResult GetManualTradingList()
        {
            var manualTradingEntries = _context.ManualTrading.OrderByDescending(e => e.addedDate).ToList();
            foreach (var item in manualTradingEntries) 
            {
                if (item.transactionType == "Buy" && item.IsExecute==1)
                {
                    var isWithSellRecord = _context.ManualTrading.Where(e => e.tradingSymbol == item.tradingSymbol && e.transactionType == "Sell").FirstOrDefault();
                    if (isWithSellRecord != null)
                    {
                        item.WithSellButton = "NoButton";
                    }
                    else 
                    {
                        item.WithSellButton = "GetSellButton";
                    }

                }
            }


            return Json(manualTradingEntries,JsonRequestBehavior.AllowGet);
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
        public ActionResult GetTokenSymbol(string indexName)
        {
            var loadedValue = _context.TradingIndex.Where(e => e.indexName == indexName).Select(e => e.symbolToken).FirstOrDefault();

            return Json(loadedValue);
        }
        public ActionResult GetIndexList(string searchTerm)
        {
            var GetTokenSymbol = _context.TradingIndex.Where(e => e.indexName == searchTerm).Select(e => e.symbolToken).FirstOrDefault();
            var loadedValue = _context.TradingIndex.Where(e => e.indexName.Contains(searchTerm)).OrderBy(e => e.expiry).Take(10).ToList().Select(e => new {id = e.indexName, text = getFullName(e.name, e.expiry, e.type, e.indexvalue ), name=e.name, tokenSymbol= GetTokenSymbol, indexName =e.indexName, indexValue=e.indexvalue, type=e.type, expiryData = e.expiry }).ToList();
           
            return Json(loadedValue, JsonRequestBehavior.AllowGet);
        }
        public string getFullName(string name, DateTime? expiryDate, string type, double? indexvalue) 
        {
            var FirstName = name;
            String dy = expiryDate.Value.Day.ToString();
            String mn = expiryDate.Value.ToString("MMM");
            String yy = expiryDate.Value.Year.ToString();
            var MiddleValue = indexvalue;
            var Type = type;
            var FullName = FirstName + " - " + dy + " " + mn + " " + yy + " - " + MiddleValue + " - " + Type;
            return FullName;
        }
        public ActionResult QueareOff()
        {
            var model = _context.Settings.FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
          
        }
        
       
        public ActionResult GetTradeBook(string search)
        {
            var model = _context.Database.SqlQuery<TradeBookViewModel>("call spGetTradeBook").Where(e => e.buyQty!= e.sellQty ).Select(e => new TradeBookViewModel { id=e.id, clientId=e.clientId, tradingSymbol=e.tradingSymbol, buyQty=e.buyQty, sellQty=e.sellQty, currentPrice=e.currentPrice, stopLoss=e.stopLoss}).ToList();
            return View();
        }
        public ActionResult GetTradeBookData()
        {
            var model = _context.Database.SqlQuery<TradeBookViewModel>("call spGetTradeBook").Where(e => e.buyQty != e.sellQty).Select(e => new TradeBookViewModel { id = e.id, clientId = e.clientId, firstName = e.firstName, lastName = e.lastName, tradingSymbol = e.tradingSymbol, buyQty = e.buyQty, sellQty = e.sellQty, currentPrice = e.currentPrice, stopLoss = e.stopLoss }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
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

        public ActionResult UserListForSelection(string searchTerm)
        {
            var model = _context.User.Where(e => e.firstName.Contains(searchTerm) || e.lastName.Contains(searchTerm) || e.angel_user.Contains(searchTerm)).Select(e => new UserListModel { id = e.angel_user, firstName = e.firstName, lastName=e.lastName }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStatus(int Id)
        {
            var model = _context.User.Where(e => e.id==Id).FirstOrDefault();
            if (model.active == "Y")
            {
                model.active = "N";
            }
            else {
                model.active = "Y";
            }
            var count = _context.SaveChanges();
            return Json(model.active);
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
        public ActionResult EditUserDetail(AddUserViewModel EditUserModel)
        {
            var user = _context.User.Where(e => e.id == EditUserModel.Id).FirstOrDefault();
            if (user != null)
            {
                user.userId = EditUserModel.userId;
                user.email = EditUserModel.email;
                user.Phone = EditUserModel.Phone;
                user.angel_user = EditUserModel.angel_user;
                user.active = EditUserModel.active;
                var data = _context.SaveChanges();
                return Json(user.userId);
            }

            return Json("NotFound");
            //_context.User.Add(AddUser);
            //var isAdded = _context.SaveChanges();
            
        }
        public ActionResult DeleteUser(int Id)
        {
            var user = _context.User.Where(e => e.id == Id).FirstOrDefault();
            _context.User.Remove(user);
            var isAdded = _context.SaveChanges();
            return Json(isAdded);
        }

        public ActionResult BuyOrSellManualTrading(int Id)
        {
            var user = _context.ManualTrading.Where(e => e.id == Id).FirstOrDefault();
            
            ManualTrading model = new ManualTrading
            {
                tradingSymbol = user.tradingSymbol,
                symbolToken = user.symbolToken,
                optionType = user.optionType,
                transactionType = user.transactionType=="Buy"?"Sell":"Buy",
                price = user.price,
                addedDate = user.addedDate,
                lotsize = user.lotsize,
                IsExecute = 0

            };
            _context.ManualTrading.Add(model);
            var isAdded = _context.SaveChanges();
            return Json(isAdded);
        }
    }
}
