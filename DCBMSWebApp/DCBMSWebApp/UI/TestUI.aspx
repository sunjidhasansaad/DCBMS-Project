<%@ Page Title="Setup Page" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TestUI.aspx.cs" Inherits="DCBMSWebApp.UI.TestUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../Assets/css/form.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div id="contact-form">
            <fieldset>
                <legend>Test Setup:</legend>
                <div class="row">

                    <div class="col-md-4">
                        <asp:Label ID="Label1" runat="server" Text="Test Name"></asp:Label>
                    </div>

                    <div class="col-md-8">
                        <asp:TextBox ID="testNameTextBox" runat="server"></asp:TextBox>

                    </div>

                </div>
                <div class="col-md-12">
                    <asp:RequiredFieldValidator ID="testNameRequiredFieldValidator" runat="server" ControlToValidate="testNameTextBox" ErrorMessage="Test Name is Required!!!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="testNameRegularExpressionValidator" runat="server" ControlToValidate="testNameTextBox"
                                                    ValidationExpression="[^0-9]+$" ErrorMessage=" Numbers are not allowed!!"/>

                </div>

                <div class="row">

                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Fee"></asp:Label>
                    </div>

                    <div class="col-md-8">
                        <asp:TextBox ID="feeTextBox" runat="server"></asp:TextBox>

                    </div>

                </div>
                <asp:RequiredFieldValidator ID="feeRequiredFieldValidator" runat="server" ControlToValidate="feeTextBox" ErrorMessage="Fee is Required!!!" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="feeRegularExpressionValidator" runat="server" ControlToValidate="feeTextBox"
                                                ValidationExpression="[+]?[0-9]*\.?[0-9]+" ErrorMessage="Enter valid amount!"/>

                <div class="row">

                    <div class="col-md-4">
                        <asp:Label ID="Label3" runat="server" Text="Test Type"></asp:Label>
                    </div>

                    <div class="col-md-8">
                        <asp:DropDownList ID="typeDropDownList" runat="server"></asp:DropDownList>

                    </div>

                </div>
                <asp:Label ID="validationLabel" runat="server" Text=""></asp:Label>
                <div class="col-md-8"></div>
                <div class="col-md-4">
                    <asp:Button ID="saveButton" runat="server" Text="Save"  BackColor="#214761" CssClass="button" ForeColor="White"/>
                </div>

            </fieldset>

        </div>


    </div> 
    <br/>
    <div class="panel panel-default">
        <h3> Test Data</h3>
        <div class="panel-body form-horizontal">

            <div>
                <asp:GridView ID="testListGridView" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="SL No.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Test Name" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Fee" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#Eval("Fee") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Type" ItemStyle-Width="80%">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

        </div>

    </div>
</asp:Content>
