using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
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
                Session["isSaved"] = false;
            }
            if (Session["isSaved"].Equals(true))
            {
                ViewState["testList"] = null;
                ViewState["total"] = null;
                testList = new List<Test>();

                totalAmountTextBox.Text = "";
                addTestGridView.DataSource = testList;
                addTestGridView.DataBind();
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
            if (patientNameTextBox.Text == "" && dateOfBirthTextBox.Text == "" && mobileNoTextBox.Text == "")
            {
                validationLabel.Text = "Enter All Information!";
            }
            else if (mobileNoTextBox.Text.Length > 11 || mobileNoTextBox.Text.Length < 11)
            {
                validationLabel.Text = "Enter Valid Mobile No.";
            }
            else
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

                        Session["isSaved"] = true;
                        GeneratePdf(aBill.BillNo, aBill.Date);
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
            
        }

        protected void testDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (testDropDownList.SelectedIndex > 0)
            {
                int testId = Convert.ToInt32(testDropDownList.SelectedValue);
                feeTextBox.Text = _testManager.GetFeeByTest(testId).ToString();
            }
        }
        public void GeneratePdf(string billNo, DateTime billDate)
        {


            string BillNo = billNo;
            string date = billDate.ToShortDateString();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Sl No",typeof(int)),
                new DataColumn("Test Name",typeof(string)),
                new DataColumn("Fee",typeof(decimal))
            });

            int sl = 0;
            foreach (var test in testList)
            {

                sl = sl + 1;

                dt.Rows.Add(sl, test.Name, test.Fee);

            }
            StringBuilder sbr = new StringBuilder();
            StringWriter sw = new StringWriter(sbr);
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            StringBuilder sb = new StringBuilder();
            sb.Append("<table width = '100%' cellspacing ='0' cellpading = '2'>");
            sb.Append("<tr><td align = 'center'><b>Bill Information</b></td></tr>");

            sb.Append("<tr><td><b>Bill No : </b>");
            sb.Append(billNo);
            sb.Append("<tr><td><b>Date : </b>");
            sb.Append(date);
            sb.Append("</td></tr>");
            sb.Append("</table>");
            sb.Append("<br/>");


            sb.Append("<table border = '1'>");
            sb.Append("<tr>");

            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th>");
                sb.Append(column.ColumnName);
                sb.Append("</th>");

            }
            sb.Append("</tr>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[column]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("<tr><td align = 'right' colspan = '");
            sb.Append(dt.Columns.Count - 1);
            sb.Append("'><b>Total : </b></td>");
            sb.Append("<td>");
            sb.Append(total);
            sb.Append("</td>");
            sb.Append("</tr></table>");

            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 40f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Bill.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();


        }

    }
}