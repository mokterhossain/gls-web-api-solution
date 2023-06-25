<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header-PSRN.aspx.cs" Inherits="GLSWebAPI.Header_PSRN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family: 'Times New Roman'">
    <form id="form1" runat="server">
        <div style="padding: 0px 20px;font-size: 20px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="border-bottom: solid 2px #cccccc; padding: 0px 0px 5px 0px;line-height:22px;">
                <tr>
                    <td style="width:15%; padding-left:10px;">
                        <img width="100" src="img/logo.png" />
                    </td>
                    <td style="width:30%; line-height: 30px;">
                        <div>GUARDIAN LAB SERVICES</div>
                        <div>11-2280 39 AVE NE</div>
                        <div>CALGARY, AB. T2E 6P7</div>
                        <div>PHONE:(403) 452-1003</div>
                    </td>
                    <td rowspan="2"  style="width:55%; vertical-align: middle; font-weight: bold; font-size: 24px; text-align:center;">
                        Project No: <%= this.project.ProjectNumber %>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style=""font-size: 16px;">
                        <div>e-mail: customerservice@guardianlab.ca</div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
