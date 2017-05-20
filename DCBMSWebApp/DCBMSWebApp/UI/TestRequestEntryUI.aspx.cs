using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DCBMSWebApp.UI
{
    public partial class TestRequestEntryUI : System.Web.UI.Page
    {
        TestManager _testManager = new TestManager();
        BillManager _billManager = new BillManager();
        PatientManager _patientManager = new PatientManager();
        TestBillManager _testBillManager = new TestBillManager();

        private decimal total;
        private List<Test> testList; 
        protected void Page_Load(object sender, EventArgs e)
        {
            validationLabel.Text = "";
            notificationLabel.Text = "";

            if (ViewState["total"] != null)
            {
                total = (decimal)ViewState["total"];
            }
            else
            {
                total = 0;
            }
            if (ViewState["testList"] != null)
            {
                testList = (List<Test>)ViewState["testList"];

            }
            else
            {
                testList = new List<Test>();
            }

            if (!IsPostBack)
            {
                GetTestDropDownList();
            }
        }

        protected void GetTestDropDownList()
        {
            List<Test> testList = _testManager.GetAll();

            testDropDownList.DataTextField = "Name";
            testDropDownList.DataValueField = "Id";
            testDropDownList.DataSource = testList;
            testDropDownList.DataBind();
            testDropDownList.Items.Insert(0, new ListItem("Select Test", ""));
            testDropDownList.SelectedIndex = 0;
        }
        protected void addButton_OnClick(object sender, EventArgs e)
        {
            if (testDropDownList.SelectedIndex > 0)
            {
                Test test = new Test();

                test.Id = Convert.ToInt32(testDropDownList.SelectedValue);
                test.Name = testDropDownList.SelectedItem.Text;
                test.Fee = Convert.ToDecimal(feeTextBox.Text);
                if (testList.All(i => i.Name != test.Name))
                {
                    total += test.Fee;
                    totalAmountTextBox.Text = total.ToString();

                    testList.Add(test);
                    ViewState["total"] = total;
                    ViewState["testList"] = testList;

                    addTestGridView.DataSource = testList;
                    addTestGridView.DataBind();
                }
                else
                {
                    validationLabel.Text = "Already Added!";
                }
            }
            else
            {
                validationLabel.Text = "select test!";
            }
        }

        protected void saveButton_OnClick(object sender, EventArgs e)
        {
            if (testList.Count > 0)
            {
                string message = "";

                Bill aBill = new Bill();

                aBill.BillNo = DateTime.Now.ToString("yyMMddhhmmssff");
                aBill.Date = DateTime.Now;
                aBill.TotalAmount = Convert.ToDecimal(totalAmountTextBox.Text);
                aBill.PaidAmount = 0;
                aBill.DueAmount = aBill.TotalAmount;
                Patient aPatient = new Patient();

                aPatient.Name = patientNameTextBox.Text;
                aPatient.DateOfBirth = Convert.ToDateTime(dateOfBirthTextBox.Text);
                aPatient.MobileNo = mobileNoTextBox.Text;
                aPatient.BillNo = aBill.BillNo;

                patientNameTextBox.Text = String.Empty;
                dateOfBirthTextBox.Text = "";
                mobileNoTextBox.Text = "";

                if (!_billManager.IsBillNoExist(aBill))
                {
                    message = _billManager.Save(aBill);

                    message += _patientManager.Save(aPatient);

                    notificationLabel.Text = message;

                    foreach (var test in testList)
                    {
                        TestBillVM testBillVm = new TestBillVM();

                        testBillVm.BillNo = aBill.BillNo;
                        testBillVm.TestId = test.Id;

                        _testBillManager.Save(testBillVm);
                    }


                    GeneratePDF(aBill.BillNo, aBill.Date);
                }
                else
                {
                    notificationLabel.Text = "Bill Already Exists !!";
                }
            }
            else
            {
                notificationLabel.Text = "Add test First !";
            }
        }

        protected void testDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (testDropDownList.SelectedIndex > 0)
            {
                int testId = Convert.ToInt32(testDropDownList.SelectedValue);
                feeTextBox.Text = _testManager.GetFeeByTest(testId).ToString();
            }
        }
        protected void GeneratePDF(string billNo, DateTime date)
        {
            Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            Paragraph Text = new Paragraph("Bill Information:\n" + "\nBill No : " + billNo + "\nDate : " + date.ToShortDateString());
            pdfDoc.Add(Text);
            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Bill.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

        }
    }
}