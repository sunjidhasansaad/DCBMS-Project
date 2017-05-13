using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.UI
{
    public partial class TestUI : System.Web.UI.Page
    {
        TestTypeManager _testTypeManager = new TestTypeManager();
        TestManager _testManager = new TestManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<TestType> types = _testTypeManager.GetAll();
                typeDropDownList.DataTextField = "Name";
                typeDropDownList.DataValueField = "Id";
                typeDropDownList.DataSource = types;
                typeDropDownList.DataBind();
                typeDropDownList.Items.Insert(0, new ListItem("Select Type", ""));
                typeDropDownList.SelectedIndex = 0;

                ShowTestListGridView();
            }
        }

        protected void ShowTestListGridView()
        {
            List<TestWithTypeVM> testList = _testManager.GetAllWithType();

            testListGridView.DataSource = testList;
            testListGridView.DataBind();
        }
        protected void saveButton_OnClick(object sender, EventArgs e)
        {
            if (typeDropDownList.SelectedIndex > 0)
            {
                Test test = new Test();

                test.Name = testNameTextBox.Text;
                test.Fee = Convert.ToDecimal(feeTextBox.Text);
                test.TypeId = Convert.ToInt32(typeDropDownList.SelectedValue);

                validationLabel.Text = _testManager.Save(test);

                ClearText();
                ShowTestListGridView();
            }
            else
            {
                validationLabel.Text = "Select a Type !";
            }
        }

        protected void ClearText()
        {
            testNameTextBox.Text = "";
            feeTextBox.Text = "";
            typeDropDownList.SelectedIndex = 0;
        }
    }
}