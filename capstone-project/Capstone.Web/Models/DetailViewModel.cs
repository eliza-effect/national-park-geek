using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DetailViewModel
    {
        public Park Park { get; set; }

        public List<Weather> FiveDaysOfWeather { get; set; }

        public bool IsFahrenheit { get; set; }

        // temperature converter
        public string ConvertTemp(bool isFahrenheit, int temp)
        {
            string s = "";

            if (isFahrenheit)
            {
                s = temp.ToString();
            }
            else
            {
                s = Math.Round((((double)temp - 32.0) * (5.0 / 9.0)), 1).ToString();
            }
            return s;
        }


        public string Recommendation(string forecast, int high, int low)
        {
            string s = "";
            if(forecast == "snow")
            {
                s = s + " *extremely Mom voice* You'd better pack snowshoes!!!!" ;
            }
            else if(forecast == "rain")
            {
                s = s + " Pack rain gear and wear waterproof shoes.";
            }
            else if(forecast == "sun")
            {
                s = s + " wear sunblock";
            }
            else if(forecast == "thunderstorms")
            {
                s = s + " seek shelter and avoid hiking on exposed ridges";
            }

            if(high > 75)
            {
                s = s + " Bring an extra gallon of water on your hike!";
            }
            if(high-low > 20)
            {
                s = s + " Expect large temperature swings. Wear breathable layers.";
            }
            if(low < 20)
            {
                s = s + " Expect frigid temperatures. Wear warm clothing that covers all extremities.";
            }

            if(s == "")
            {
                s = " There are no weather alerts today.";
            }

            return s;
        }
    }
}