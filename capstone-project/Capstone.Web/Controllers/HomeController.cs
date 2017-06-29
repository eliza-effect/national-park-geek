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

        public HomeController(IParkDAL parkDAL, IWeatherDAL weatherDAL, ISurveyDAL surveyDAL)
        {
            this.parkDAL = parkDAL;
            this.weatherDAL = weatherDAL;
            this.surveyDAL = surveyDAL;
        }
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            List<Park> parks = parkDAL.GetAllParks();
            return View("Index", parks);
        }

        [HttpGet]
        public ActionResult Detail(string parkCode)
        {
            DetailViewModel p = new DetailViewModel()
            {
                Park = parkDAL.GetPark(parkCode),
                FiveDaysOfWeather = weatherDAL.GetForecast(parkCode),
                IsFahrenheit = true
            };
            return View("Detail", p);
        }

        [HttpPost]
        public ActionResult Detail(string parkCode, bool isFahrenheit)
        {
            DetailViewModel newModel = new DetailViewModel();
            newModel.IsFahrenheit = isFahrenheit;
            newModel.Park = parkDAL.GetPark(parkCode);
            newModel.FiveDaysOfWeather = weatherDAL.GetForecast(parkCode);

            return View("Detail", newModel);
        }

        [HttpGet]
        public ActionResult Survey()
        {
            List<Park> parks = parkDAL.GetAllParks();
            Survey s = new Survey()
            {
                Parks = ConvertListToSelectList(parks)
            };

            return View("Survey", s);
        }

        [HttpPost]
        public ActionResult Survey(Survey survey)
        {
            List<Park> parks = parkDAL.GetAllParks();
            survey.Parks = ConvertListToSelectList(parks);

            if (!ModelState.IsValid)
            {
                TempData["Incomplete"] = "Some required fields were not entered correctly. Please fix the fields marked with *.";
                return View ("Survey", survey);
            }

            surveyDAL.SaveSurvey(survey);

            TempData["StatusMessage"] = "Success! Your survey has been submitted!";

            return RedirectToAction("FavoriteParks");
        }

        [HttpGet]
        public ActionResult FavoriteParks()
        {
            Dictionary<Park, int> parkVotes = new Dictionary<Park, int>();

            List<Park> parks = parkDAL.GetAllParks();
            
            foreach(Park p in parks)
            {
                int vote = surveyDAL.GetVotes(p.ParkCode);

                if(vote > 0)
                {
                    parkVotes.Add(p, vote);
                }
            }

            return View("FavoriteParks", parkVotes);
        }


        // private helper methods

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