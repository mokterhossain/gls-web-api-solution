<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer-Print-Batch.aspx.cs" Inherits="GLSWebAPI.Footer_Print_Batch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family: 'Times New Roman'">
    <form id="form1" runat="server">
        <div style="padding: 0px 20px;">
            <table cellpadding="0" cellspacing="0" width="100%">           
                 <tr>
                     <td style="width:70%">&nbsp;</td>
                    <td style="text-align:right">
                        <div style="text-align:left; font-size: 22px; width:300px;">
                        <hr />
                        Sadiqur Rahman<br />
                        Laboratory Manager
                            </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;padding-bottom:0px;">
                        <%= DateTime.Now.ToShortDateString() %>&nbsp;<%= DateTime.Now.ToLongTimeString() %>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
