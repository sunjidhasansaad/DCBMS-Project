<%@ Page Title="Report" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="UnpaidBillReportUI.aspx.cs" Inherits="DCBMSWebApp.UI.UnpaidBillReportUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Assets/css/form.css" rel="stylesheet" />
    <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.js"></script>


    <script>
        $(function () {
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
                    .on("change", function () {
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
                    .on("change", function () {
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
            
            <legend>Unpaid Bill Report:</legend>

            <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
            <asp:TextBox ID="fromDateTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="dateFromRequiredFieldValidator" runat="server" ControlToValidate="fromDateTextBox" ErrorMessage="Enter Date Range!" ForeColor="Red"></asp:RequiredFieldValidator>
            
            <br/>
            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
            <asp:TextBox ID="toDateTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="dateToRequiredFieldValidator" runat="server" ControlToValidate="toDateTextBox" ErrorMessage="Enter Date Range!" ForeColor="Red"></asp:RequiredFieldValidator>

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
            
            <asp:GridView ID="unpaidBillReportGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                    <asp:TemplateField HeaderText="SL No." ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bill Number" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("BillNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contact No" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Patient Name" ItemStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("PatientName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bill Amount" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("BillAmount") %>'></asp:Label>
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
                 <asp:Button ID="pdfButton" runat="server" Text="PDF" BackColor="#214761" Font-Size="Medium" ForeColor="White" style="float: right;" Width="60%" />
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
