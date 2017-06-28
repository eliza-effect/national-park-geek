using Capstone.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class ParkSQLDAL: IParkDAL
    {
        private string connectionString;
        private const string SQL_GetAllParks = "Select * from park;";
        private const string SQL_GetPark = "SELECT * FROM park WHERE parkCode = @parkCode;";

        public ParkSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> allParks = new List<Park>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    allParks = conn.Query<Park>(SQL_GetAllParks).ToList();
                }

                return allParks;
            }

            catch(SqlException ex)
            {
                throw;
            }
        }

        public Park GetPark(string parkCode)
        {
            Park p = new Park();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                p = conn.QueryFirstOrDefault<Park>(SQL_GetPark, new { @parkCode = parkCode });
            }
            return p;
        }

    }
}