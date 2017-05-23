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
            if (!IsPostBack)
            {
                payButton.Enabled = false;
            }
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
                payButton.Enabled = true;
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
            if (billNoTextBox.Text != "")
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
                    notificationLabel.Text = _paymentManager.PayAmount(aBill);

                    Bill bill = _billManager.GetBillByBillNo(aBill.BillNo);

                    billDateLabel.Text = bill.Date.ToShortDateString();
                    totalFeeLabel.Text = bill.TotalAmount.ToString();
                    paidAmountLabel.Text = bill.PaidAmount.ToString();
                    DueAmountLabel.Text = bill.DueAmount.ToString();

                }
            }
            else
            {
                notificationLabel.Text = "Please Enter a Bill No.";
            }
        }
    }
}