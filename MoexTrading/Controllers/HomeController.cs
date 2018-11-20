using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoexTrading.Models;


using ru.micexrts.cgate;

namespace MoexTrading.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult TotalInfo()
        {
            ViewBag.List = APIMongo.GetTools();

            return View();
        }
    }
}
