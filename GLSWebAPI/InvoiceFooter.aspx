<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceFooter.aspx.cs" Inherits="GLSWebAPI.InvoiceFooter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="font-family:'Times New Roman', Times, serif">
        <div style="padding:0 100px;">
            Note: Payment terms are 30 days from the date of invoice. In case of any inquiry concerning this invoice , please contact us at
(403)-452-1003 or email us at customerservice@glstesting.ca
        </div>
        <div style="margin-top:15px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 53%"></td>
                    <td style="text-align: center;">
                        <div>Issued By (Signature)</div>
                        <div style="margin-top:25px;">
                            <img style="width: 200px;max-height: 80px;" src="<%= accountsManagerSignUrl %>" />
                        </div>
                        <div style="border-top: solid 1px #d3d3d3">
                            <%= this.clientInvoiceData.AccountsManagerName %>, <%= this.clientInvoiceData.AccountsManagerDiploma %>
                        </div>
                    </td>
                    <td style="width:17%"></td>
                </tr>
            </table>
        </div>
        <div style="text-align:center;font-weight:bold;margin-top:15px;"><i>Thank you for your business.</i></div>
        <div style="text-align:right;padding-right:25px;margin-top: 20px">
            <div><i>Guardian Laboratory : 78744 5311 RT0001</i></div>
            <div><i>1260033 Alberta Ltd: 84593 4363 RT0001</i></div>

        </div>
    </form>
</body>
</html>
