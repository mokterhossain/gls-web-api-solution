<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceHeader.aspx.cs" Inherits="GLSWebAPI.InvoiceHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 0px 20px;font-size: 20px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="border-bottom: solid 2px #cccccc; padding: 0px 0px 5px 0px;line-height:22px;">
                <tr>
                    <td style="width:15%; padding-left:10px;background-color: #6495ED">
                        <img width="100" src="img/logo.png" style="background-color:#ffffff" />
                    </td>
                    <td style="width:50%; line-height: 30px; padding-top: 10px; text-align: center; background-color: #6495ED; color: #ffffff;">
                        <div>GUARDIAN LAB SERVICES</div>
                        <div>11-2280 39 AVE NE</div>
                        <div>CALGARY, AB. T2E 6P7</div>
                        <div>PHONE:(403) 452-1003</div>
                        <div style="padding-left: 14px;">e-mail: customerservice@guardianlab.ca</div>
                    </td>
                    <td style="width:35%;text-align: center; background-color: #6A5ACD; color: #ffffff;">
                        <h1 style="font-size:48px;">Invoice</h1>
                        <div>Invoice No</div>
                        <div><%= this.clientInvoiceData.InvoiceNumber %></div>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2" style=""font-size: 16px;">
                        <div style="padding-left: 14px;">e-mail: customerservice@guardianlab.ca</div>
                    </td>
                </tr>--%>
            </table>            
        </div>
    </form>
</body>
</html>
