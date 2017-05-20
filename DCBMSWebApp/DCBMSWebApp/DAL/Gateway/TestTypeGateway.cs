using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class TestTypeGateway
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;
        public int Save(TestType testType)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT INTO TestTypes(Type) VALUES('"
                 + testType.Name + "')";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public bool IsTypeExist(TestType testType)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM TestTypes WHERE Type='" +testType.Name+ "';";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }

        public List<TestType> GetAll()
        {
            List<TestType> typeList = new List<TestType>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM TestTypes ORDER BY Type";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TestType testType = new TestType();
                
                testType.Id = (int)reader["Id"];
                testType.Name = reader["Type"].ToString();
                
                typeList.Add(testType);
            }
            reader.Close();
            connection.Close();
            return typeList;
        }
    }
}