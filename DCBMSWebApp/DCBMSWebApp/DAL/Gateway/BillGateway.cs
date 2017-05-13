using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class BillGateway
    {
        private string connectionString =
          WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;
        public int Save(Bill bill)
        {
            DateTime date = new DateTime();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT INTO Bills(BillNo,Date,TotalAmount,PaidAmount,DueAmount) VALUES('"
                + bill.BillNo + "','" + @date + "','" + bill.TotalAmount + "','" + bill.PaidAmount + "','" + bill.DueAmount + "')";
            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            command.Parameters.AddWithValue("@date", bill.Date);
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public bool IsBillNoExist(Bill bill)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM Bills WHERE BillNo='" + bill.BillNo + "' AND Id<>'" + bill.Id + "';";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
    }
}