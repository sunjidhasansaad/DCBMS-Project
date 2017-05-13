using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class PatientGateway
    {
        private string connectionString =
          WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;
        public int Save(Patient aPatient)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT INTO Patients(Name,DateOfBirth,MobileNo,BillNo) VALUES('"
                + aPatient.Name + "','" + aPatient.DateOfBirth + "','" + aPatient.MobileNo + "','" + aPatient.BillNo + "')";
            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }
    }
}