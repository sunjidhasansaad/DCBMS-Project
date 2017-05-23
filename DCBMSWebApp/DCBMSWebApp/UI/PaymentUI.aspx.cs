using System;
using System.Collections.Generic;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.UI
{
    public partial class PaymentUI : System.Web.UI.Page
    {
        PaymentManager _paymentManager = new PaymentManager();
        BillManager _billManager = new BillManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchButton_OnClick(object sender, EventArgs e)
        {
            string billNo = billNoTextBox.Text;

            Bill aBill = _billManager.GetBillByBillNo(billNo);
            if (aBill != null)
            {
                billDateLabel.Text = aBill.Date.ToShortDateString();
                totalFeeLabel.Text = aBill.TotalAmount.ToString();
                paidAmountLabel.Text = aBill.PaidAmount.ToString();
                DueAmountLabel.Text = aBill.DueAmount.ToString();
                validationLabel.Text = "";
            }
            else
            {
                validationLabel.Text = "Bill Number NOT FOUND!!!";
            }

            List<Test> tests = _paymentManager.GetTestsByBillNo(billNo);

            testListGridView.DataSource = tests;
            testListGridView.DataBind();



        }

        protected void payButton_OnClick(object sender, EventArgs e)
        {
            if (payAmountTextBox.Text == "")
            {
                notificationLabel.Text = "Please Give Amount";
            }

            else
            {
                Bill aBill = new Bill();
                aBill.BillNo = billNoTextBox.Text;
                aBill.TotalAmount = Convert.ToDecimal(totalFeeLabel.Text);
                decimal lastPaidAmount = Convert.ToDecimal(paidAmountLabel.Text);
                aBill.PaidAmount = Convert.ToDecimal(payAmountTextBox.Text) + lastPaidAmount;
                aBill.DueAmount = Convert.ToDecimal(DueAmountLabel.Text);
                _paymentManager.PayAmount(aBill);


                billDateLabel.Text = aBill.Date.ToShortDateString();
                totalFeeLabel.Text = aBill.TotalAmount.ToString();
                paidAmountLabel.Text = aBill.PaidAmount.ToString();
                DueAmountLabel.Text = aBill.DueAmount.ToString();
                notificationLabel.Text = _paymentManager.PayAmount(aBill);

            }
        }
    }
}