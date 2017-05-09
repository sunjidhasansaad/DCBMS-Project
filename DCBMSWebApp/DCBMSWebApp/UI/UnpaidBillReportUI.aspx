<%@ Page Title="Report" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="UnpaidBillReportUI.aspx.cs" Inherits="DCBMSWebApp.UI.UnpaidBillReportUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Assets/css/form.css" rel="stylesheet" />
    <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.js"></script>


    <script>
        $(function () {
            var dateFormat = "dd/mm/yy",
                from = $("#<%= fromDateNameTextBox.ClientID %>")
                    .datepicker({
                        dateFormat: 'dd/mm/yy',
                        defaultDate: "+1w",
                        changeMonth: true,
                        changeYear: true,
                        numberOfMonths: 1
                    })
                    .on("change", function () {
                        to.datepicker("option", "minDate", getDate(this));
                    }),
                to = $("#<%= toDateTextBox.ClientID %>").datepicker({
                    dateFormat: 'dd/mm/yy',
                    defaultDate: "+1w",
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
            <asp:TextBox ID="fromDateNameTextBox" runat="server"></asp:TextBox>

            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
            <asp:TextBox ID="toDateTextBox" runat="server"></asp:TextBox>
            
            <div>
                <asp:Button ID="showButton" runat="server" Text="Show"  BackColor="#214761" Font-Size="Medium" ForeColor="White" Width="30%" style="float: right;"/>
            </div>
        </fieldset>

    </div>
    </div>
    <br/>
     <div class="panel panel-default">

        <div class="panel-body form-horizontal">
            
            
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
