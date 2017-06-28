using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAL parkDAL;

        public HomeController(IParkDAL parkDal)
        {
            this.parkDAL = parkDal;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<Park> parks = parkDAL.GetAllParks();
            return View("Index", parks);
        }
    }
}