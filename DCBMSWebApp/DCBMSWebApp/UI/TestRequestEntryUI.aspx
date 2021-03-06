﻿<%@ Page Title="Test Entry Page" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TestRequestEntryUI.aspx.cs" Inherits="DCBMSWebApp.UI.TestRequestEntryUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link href="../Assets/css/form.css" rel="stylesheet" />
    <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.js"></script>

    

    <script>
        $(function() {
            $("#<%= dateOfBirthTextBox.ClientID %>").datepicker({
                dateFormat: 'mm/dd/yy',
                maxDate: "+0M +0D",
                changeMonth: true,
                changeYear: true
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div id="contact-form">
            <fieldset>
                <legend>Test Request Entry:</legend>

                <asp:Label ID="Label1" runat="server" Text="Name of the Patient"></asp:Label>
                <asp:TextBox ID="patientNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="patientRequiredFieldValidator" runat="server" ControlToValidate="patientNameTextBox" ErrorMessage="Patient Name is Required!!" ForeColor="Red"></asp:RequiredFieldValidator>
                
                <br/>
                <asp:Label ID="Label2" runat="server" Text="Date of Birth"></asp:Label>

                <asp:TextBox ID="dateOfBirthTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DOBRequiredFieldValidator" runat="server" ControlToValidate="dateOfBirthTextBox" ErrorMessage="Date of Birth is Required!!!" ForeColor="Red"></asp:RequiredFieldValidator>
                
                <br/>
                <asp:Label ID="Label3" runat="server" Text="Mobile No"></asp:Label>
                <asp:TextBox ID="mobileNoTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="mobileRequiredFieldValidator" runat="server" ControlToValidate="mobileNoTextBox" ErrorMessage="Mobile No is Required!!" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="mobileRegularExpressionValidator" runat="server" 
      ControlToValidate="mobileNoTextBox" ErrorMessage=" Enter Valid Mobile Number!!" 
    ValidationExpression="[0-9]{11}"></asp:RegularExpressionValidator>

                <br/>
                <asp:Label ID="Label4" runat="server" Text="Select Test"></asp:Label>
                <asp:DropDownList ID="testDropDownList" runat="server" ></asp:DropDownList>

                <div class="row">
                    <div class="col-md-6"></div>
                    <div class="col-md-3 text-right">
                        <asp:Label ID="Label5" runat="server" Text="Fee"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="feeTextBox" runat="server" ReadOnly="True" style="float: right"></asp:TextBox>
                    </div>
                </div>

                <div>
                    
                        <asp:Button ID="addButton" runat="server" Text="ADD"  BorderStyle="Solid" BackColor="#214761" Font-Bold="True" Font-Size="Medium" ForeColor="White" ToolTip="Add in table" Width="30%" style="float: right;"/>
                   
                </div>
            </fieldset>
        </div>
    </div>
    <br/>
    <div class="panel panel-default">

        <div class="panel-body form-horizontal">
            

            


            <asp:Button ID="saveButton" runat="server" Text="Save" BackColor="#214761"  ForeColor="White" style="float: right;" Width="20%" Font-Size="Medium" Font-Bold="True"/>
        </div>
    </div>
</asp:Content>
