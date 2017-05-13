using System;
using System.Collections.Generic;
using DCBMSWebApp.BLL;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.UI
{
    public partial class TestTypeUI : System.Web.UI.Page
    {
        TestTypeManager _testTypeManager = new TestTypeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowTypeInGridView();
            }
            
        }

        protected void ShowTypeInGridView()
        {
            List<TestType> typeList = _testTypeManager.GetAll();

            typeListGridView.DataSource = typeList;
            typeListGridView.DataBind();
        }
        protected void saveButton_OnClick(object sender, EventArgs e)
        {
            TestType testType = new TestType();

            testType.Name = typeTextBox.Text;

            validationLabel.Text = _testTypeManager.Save(testType);

            ShowTypeInGridView();
        }
    }
}