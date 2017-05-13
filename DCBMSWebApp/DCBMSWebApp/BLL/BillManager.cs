using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.BLL
{
    public class BillManager
    {
        BillGateway _billGateway = new BillGateway();
        public string Save(Bill bill)
        {
            if (!_billGateway.IsBillNoExist(bill))
            {
                int rowAffected = _billGateway.Save(bill);

                if (rowAffected > 0)
                {
                    return "Bill Saved Succesfully.";
                }

            }
            return "Failed to save bill !!";
        }

        public bool IsBillNoExist(Bill bill)
        {
            return _billGateway.IsBillNoExist(bill);
        }
    }
}