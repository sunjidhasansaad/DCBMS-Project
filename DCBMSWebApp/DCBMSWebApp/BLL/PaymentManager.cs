using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.BLL
{
    public class PaymentManager
    {
        PaymentGateway _paymentGateway = new PaymentGateway();
        public List<Test> GetTestsByBillNo(string billNo)
        {
            return _paymentGateway.GetTestsByBillNo(billNo);
        }

        public string PayAmount(Bill abill)
        {
            if (abill.BillNo == null)
            {
                return "please select a Bill Number";
            }

            else if (abill.PaidAmount == abill.TotalAmount || abill.PaidAmount < abill.TotalAmount)
            {
                abill.DueAmount = abill.TotalAmount - abill.PaidAmount;
            }
            else if (abill.PaidAmount > abill.TotalAmount || abill.PaidAmount < 0)
            {
                return "Please Give a Valid Input";
            }
            int rowAffected = _paymentGateway.PayAmount(abill);
            if (rowAffected > 0)
            {
                return "Update Successfull";
            }

            return "Payment Unseccessfull";

        }
    }
}