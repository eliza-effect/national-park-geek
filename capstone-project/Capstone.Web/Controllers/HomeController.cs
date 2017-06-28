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
        private IWeatherDAL weatherDAL;
        private ISurveyDAL surveyDAL;

        public HomeController(IParkDAL parkDAL, IWeatherDAL weatherDAL)
        {
            this.parkDAL = parkDAL;
            this.weatherDAL = weatherDAL;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<Park> parks = parkDAL.GetAllParks();
            return View("Index", parks);
        }

        public ActionResult Detail(string parkCode)
        {
            DetailViewModel p = new DetailViewModel()
            {
                Park = parkDAL.GetPark(parkCode),
                FiveDaysOfWeather = weatherDAL.GetForecast(parkCode)
            };
            return View("Detail", p);
        }


        public ActionResult Survey()
        {
            
            Survey s = new Survey();
            s.Parks = ConvertListToSelectList(parkDAL.GetAllParks());
            return View("Survey", s);
        }

        [HttpPost]
        public ActionResult Survey(Survey s)
        {
            surveyDAL.SaveSurvey(s);

            TempData["StatusMessage"] = "Success! Your survey has been submitted!";

            return RedirectToAction("FavoriteParks");
        }

        public ActionResult FavoriteParks()
        {
            return View("FavoriteParks");
        }


        private List<SelectListItem> ConvertListToSelectList(List<Park> ParksList)
        {
            List<SelectListItem> dropdownlist = new List<SelectListItem>();

            foreach (Park park in ParksList)
            {
                SelectListItem choice = new SelectListItem() { Text = park.ParkName, Value = park.ParkCode };
                dropdownlist.Add(choice);
            }

            return dropdownlist;
        }
        
    }

}