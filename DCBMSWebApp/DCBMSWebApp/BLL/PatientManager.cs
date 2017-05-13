using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.BLL
{
    public class PatientManager
    {
        PatientGateway _patientGateway = new PatientGateway();
        public string Save(Patient aPatient)
        {
            int rowAffected = _patientGateway.Save(aPatient);

            if (rowAffected > 0)
            {
                return "Patient Saved Successfully.";

            }
            return "Failed to save Patient !!";

        }
    }
}