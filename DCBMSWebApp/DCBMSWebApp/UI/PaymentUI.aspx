<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PaymentUI.aspx.cs" Inherits="DCBMSWebApp.UI.PaymentUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/form.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div id="contact-form">
            <fieldset>
                <legend>Pay Bill:</legend>
                <asp:Label ID="Label1" runat="server" Text="Bill No"></asp:Label>
                <asp:TextBox ID="billNoTextBox" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="searchButton" runat="server" Text="Search" BackColor="#214761" ForeColor="White" Width="40%" style="float: right;"/>
                </div>
                <asp:Label ID="validationLabel" runat="server" Text=""></asp:Label>

            </fieldset>
        </div>
    </div>

    <br/>
    <div class="panel panel-default">

        <div class="panel-body form-horizontal">

            <div>

                <asp:GridView ID="testListGridView" runat="server"></asp:GridView>
            

            </div>

        </div>

        <div class="container-fluid">
            <fieldset>
                <legend>Bill Information</legend>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-6 text-right">
                            <asp:Label ID="Label2" runat="server" Text="Bill Date"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="billDateLabel" runat="server" Text="Bill Date"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 text-right">
                            <asp:Label ID="Label3" runat="server" Text="Total Fee"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="totalFeeLabel" runat="server" Text="Amount"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 text-right">
                            <asp:Label ID="Label4" runat="server" Text="Paid Amount"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="paidAmountLabel" runat="server" Text="Amount"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 text-right">
                            <asp:Label ID="Label5" runat="server" Text="Due Amount"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="DueAmountLabel" runat="server" Text="Due Amount"></asp:Label>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-6 text-right">
                            <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="payAmountTextBox" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="feeRegularExpressionValidator" runat="server" ControlToValidate="payAmountTextBox"
                                                ValidationExpression="[+]?[0-9]*\.?[0-9]+" ErrorMessage="Enter valid amount!"/>
                    
                        </div>
                        </div>
                    <br/>
                    <asp:Button ID="payButton" runat="server" Text="Pay" BackColor="#214761" Font-Bold="True" Font-Size="Medium" ForeColor="White" Width="20%" style="float: right;"/>
                    
                </div>

            </fieldset>

        </div>

        <br/>

    </div>
</asp:Content>
