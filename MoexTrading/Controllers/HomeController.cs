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


            ViewBag.Title = "Вход";

            return View();
        }

        public ActionResult TotalInfo()
        {
            ViewBag.NameInstrument = APIMongo.GetData<DataTools>(ElementMongo.NameTableTools);

            return View();
        }

        public ActionResult Kotirovka()
        {

            return View();
        }
    }
}
