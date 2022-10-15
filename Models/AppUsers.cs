using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace GoldDashboard.Models
{
    public class AppUsers: IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUsers> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string GoldUserName { get; set; }
        public int GoldUserId { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal PerDayProfit { get; set; }
        public decimal PerWeekProfit { get; set; }
        public decimal PerMonthProfit { get; set; }
        public decimal DailyProfit { get; set; }
        public decimal Percent { get; set; }

        [NotMapped]
        public decimal USD_Rate { get; set; }

        [NotMapped]
        public decimal ConvertedBalance
        {
            get
            {
                return Math.Round(Balance*USD_Rate,2);
            }
        }
        [NotMapped]
        public decimal ConvertedDeposit
        {
            get
            {
                return Math.Round(Deposit * USD_Rate,2);
            }
        }

        [NotMapped]
        public int CurrentYear
        {
            get
            {
                return DateTime.Now.Year;
            }
        }
        [NotMapped]
        public int CurrentMonth
        {
            get
            {
                return DateTime.Now.Month;
            }
        }
        [NotMapped]
        public int TotalDaysInMonth
        {
            get
            {
                return DateTime.DaysInMonth(CurrentYear, CurrentMonth);
            }
        }
        [NotMapped]
        public int TotalWorkignDay
        {

            get
            {
                int daysInMonth = 0;
                int days = TotalDaysInMonth;
                for (int i = 1; i <= days; i++)
                {
                    DateTime day = new DateTime(CurrentYear, CurrentMonth, i);
                    if (day.DayOfWeek != DayOfWeek.Sunday && day.DayOfWeek != DayOfWeek.Saturday)
                    {
                        daysInMonth++;
                    }
                }
                return daysInMonth;
            }
        }
        [NotMapped]
        public decimal DayProfit
        {
            get
            {
                return Math.Round((ConvertedBalance-ConvertedDeposit)/ TotalWorkignDay,2);
            }
        }
        [NotMapped]
        public decimal WeekProfit
        {
            get
            {
                return Math.Round((ConvertedBalance - ConvertedDeposit) / (TotalWorkignDay/7),2);
            }
        }
        [NotMapped]
        public decimal MonthProfit
        {
            get
            {
                return Math.Round((ConvertedBalance - ConvertedDeposit),2);
            }
        }

        [NotMapped]
        public decimal PercentProfit
        {
            get
            {
                return Math.Round((ConvertedBalance - ConvertedDeposit)/ConvertedDeposit*100, 2);
            }
        }

    }
}