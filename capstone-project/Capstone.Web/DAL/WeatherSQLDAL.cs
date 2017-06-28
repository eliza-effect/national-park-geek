using Capstone.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class WeatherSQLDAL : IWeatherDAL
    {
        private string connectionString;
        private const string SQL_GetForecast = "Select * from weather where parkCode = @parkCode;";


        public WeatherSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> GetForecast(string parkCode)
        {
            List<Weather> forecast = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    forecast = conn.Query<Weather>(SQL_GetForecast, new { @parkCode = parkCode }).ToList();
                }

                return forecast;
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public Weather GetWeather(string parkCode, int day)
        {
            Weather w = new Weather();
            return w;
        }
    }
}

