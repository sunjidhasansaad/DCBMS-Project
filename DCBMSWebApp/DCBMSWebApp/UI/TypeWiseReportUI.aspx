<%@ Page Title="Report" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TypeWiseReportUI.aspx.cs" Inherits="DCBMSWebApp.UI.TypeWiseReportUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Assets/css/form.css" rel="stylesheet" />
    <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.js"></script>


    <script>
        $(function() {
            var dateFormat = "dd/mm/yy",
                from = $("#<%= fromDateTextBox.ClientID %>")
                    .datepicker({
                        dateFormat: 'mm/dd/yy',
                        defaultDate: "+1w",
                        maxDate: "+0M +0D",
                        changeMonth: true,
                        changeYear: true,
                        numberOfMonths: 1
                    })
                    .on("change", function() {
                        to.datepicker("option", "minDate", getDate(this));
                    }),
                to = $("#<%= toDateTextBox.ClientID %>").datepicker({
                        dateFormat: 'mm/dd/yy',
                        defaultDate: "+1w",
                        maxDate: "+0M +0D",
                        changeMonth: true,
                        changeYear: true,
                        numberOfMonths: 1
                    })
                    .on("change", function() {
                        from.datepicker("option", "maxDate", getDate(this));
                    });

            function getDate(element) {
                var date;
                try {
                    date = $.datepicker.parseDate(dateFormat, element.value);
                } catch (error) {
                    date = null;
                }

                return date;
            }
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <div class="container-fluid">
        <div id="contact-form">
        
        <fieldset>
            
            <legend>Type Wise Report:</legend>

            <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
            <asp:TextBox ID="fromDateTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fromDateTextBox" ErrorMessage="Date is required!" ForeColor="Red"></asp:RequiredFieldValidator>
            
            <br />
            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
            <asp:TextBox ID="toDateTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="toDateTextBox" ErrorMessage="Date is required!" ForeColor="Red"></asp:RequiredFieldValidator>

            <div>
                <asp:Button ID="showButton" runat="server" Text="Show"  BackColor="#214761" Font-Size="Medium" ForeColor="White" Width="30%" style="float: right;" OnClick="showButton_OnClick"/>
            </div>
            
            <asp:Label ID="validationLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
        </fieldset>

    </div>
    </div>
    <br/>
     <div class="panel panel-default">

        <div class="panel-body form-horizontal">
            
            <asp:GridView ID="typeWiseReportGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">

                <Columns>
                    <asp:TemplateField HeaderText="SL No." ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Test Type Name" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total No of Test" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TotalNoOfTest") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <FooterStyle BackColor="White" ForeColor="#000066"></FooterStyle>

                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066"></PagerStyle>

                <RowStyle ForeColor="#000066"></RowStyle>

                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                <SortedAscendingHeaderStyle BackColor="#007DBB"></SortedAscendingHeaderStyle>

                <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                <SortedDescendingHeaderStyle BackColor="#00547E"></SortedDescendingHeaderStyle>
            </asp:GridView>
        </div>
         
         <br/>
         <div class="row">
             <div class="col-md-4">
                 <asp:Button ID="pdfButton" runat="server" Text="PDF" BackColor="#214761" Font-Size="Medium" ForeColor="White" style="float: right;" Width="60%" OnClick="pdfButton_OnClick"/>
             </div>
             <div class="col-md-4 text-right">
                 <asp:Label ID="Label3" runat="server" Text="Total"></asp:Label>
             </div>
             <div class="col-md-4">
                 <asp:TextBox ID="totalTextBox" runat="server" ReadOnly="True"></asp:TextBox>
             </div>
         </div>
         
         <br/>
         
    </div>
</asp:Content>
