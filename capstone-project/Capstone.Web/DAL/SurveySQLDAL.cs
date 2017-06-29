using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using Dapper;

namespace Capstone.Web.DAL
{
    public class SurveySQLDAL : ISurveyDAL
    {
        private string connectionString;

        private const string SQL_SaveSurvey = "INSERT INTO survey_result VALUES(@parkCode, @emailAddress, @state, @activityLevel); SELECT CAST(SCOPE_IDENTITY() as int);";
        private const string SQL_GetAllSurveys = "Select * from survey_result;";


        public SurveySQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveSurvey(Survey s)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    conn.Execute(SQL_SaveSurvey, new
                    {
                        parkCode = s.ParkCode,
                        emailAddress = s.EmailAddress,
                        state = s.State,
                        activityLevel = s.ActivityLevel
                    });

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public List<Survey> AllSurveys()
        {
            List<Survey> allSurveys = new List<Survey>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    allSurveys = conn.Query<Survey>(SQL_GetAllSurveys).ToList();
                }

                return allSurveys;
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public int GetVotes(string parkCode)
        {
            int votes = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    votes = conn.Query<int>("Select Count(parkCode) from survey_result where parkCode = @parkCode;", new { parkCode = parkCode }).First();
                }
                return votes;
            }

            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}