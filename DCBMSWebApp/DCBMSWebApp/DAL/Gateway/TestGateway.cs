using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class TestGateway
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;
        public int Save(Test test)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT INTO Tests(Name,Fee,TypeId) VALUES('"
                 + test.Name + "','" + test.Fee + "','" + test.TypeId +  "')";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public bool IsTestExist(Test test)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM Tests WHERE Name='" + test.Name + "';";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }

        public List<Test> GetAll()
        {
            List<Test> testList = new List<Test>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM Tests";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Test test = new Test();

                test.Id = (int)reader["Id"];
                test.Name = reader["Name"].ToString();
                test.Fee = (decimal) reader["Fee"];
                test.TypeId = (int) reader["TypeId"];

                testList.Add(test);
            }
            reader.Close();
            connection.Close();
            return testList;
        }
        public List<TestWithTypeVM> GetAllWithType()
        {
            List<TestWithTypeVM> testList = new List<TestWithTypeVM>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM TestWithType";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TestWithTypeVM test = new TestWithTypeVM();

                
                test.Name = reader["Name"].ToString();
                test.Fee = (decimal)reader["Fee"];
                test.Type = reader["Type"].ToString();

                testList.Add(test);
            }
            reader.Close();
            connection.Close();
            return testList;
        }

        public decimal GetFeeByTest(int testId)
        {
            decimal fee = 0;
            string query = "select fee from Tests where Id = ('" + testId + "')";
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            if (dataReader.HasRows)
            {
                fee = (decimal)dataReader["Fee"];
            }
            return fee;
        }
    }
}