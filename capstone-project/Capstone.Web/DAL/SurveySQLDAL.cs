using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using Dapper;

namespace Capstone.Web.DAL
{
    public class SurveySQLDAL
    {
        readonly string connectionString;

        const string SQL_SaveSurvey = "INSERT INTO survey_result VALUES(parkCode = @parkCode, emailAddress = @emailAddress, state = @state, activityLevel = @activityLevel); SELECT CAST(SCOPE_IDENTITY() as int);";

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
                    s.SurveyId = conn.Query<int>(SQL_SaveSurvey, new
                    {
                        parkCode = s.ParkCode,
                        emailAddress = s.EmailAddress,
                        state = s.State,
                        activityLevel = s.ActivityLevel
                    }).First();

                }
            }
            catch
            {
                throw;
            }
        }
    }
}