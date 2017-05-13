<%@ Page Title="Setup Page" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TestTypeUI.aspx.cs" Inherits="DCBMSWebApp.UI.TestTypeUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../Assets/css/form.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div id="contact-form">
            <fieldset>
                <legend>Test Type Setup:</legend>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label1" runat="server" Text="Type Name"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="typeTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-6 "></div>
                    <div class="col-md-6">
                        <asp:Button ID="saveButton" runat="server" Text="Save" BackColor="#214761" ForeColor="White" OnClick="saveButton_OnClick"/>
                    </div>

                </div>
                <asp:Label ID="validationLabel" runat="server" Text=""></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="typeTextBox" ErrorMessage="Type Name is Required!!!" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="typeRegularExpressionValidator" runat="server"
                                                ControlToValidate="typeTextBox"
                                                ValidationExpression="[^0-9]+$" ErrorMessage=" Numbers are not allowed!!"/>

                

            </fieldset>

        </div>
    </div>
    <br/>

    <div class="container-fluid">

        <h3>Test Type Data</h3>
        <div class="panel panel-default">

            <div class="panel-body form-horizontal">

                <asp:GridView ID="typeListGridView" runat="server">
                    
                </asp:GridView>
            </div>

        </div>

    </div>
</asp:Content>
