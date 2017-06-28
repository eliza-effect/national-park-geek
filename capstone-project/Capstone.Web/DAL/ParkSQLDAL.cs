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

        public Park GetPark(string parkID)
        {
            Park p = new Park();
            return p;
        }

    }
}