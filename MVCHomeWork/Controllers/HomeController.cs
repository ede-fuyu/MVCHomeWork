using MVCHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Controllers
{
    [TimeLogToDebug]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //test
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "Error2")]
        public ActionResult error1(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("參數錯誤");
            }
            else
            {
                throw new InvalidOperationException("操作錯誤");
            }
        }

    }
}