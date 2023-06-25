<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer-PSRN.aspx.cs" Inherits="GLSWebAPI.Footer_PSRN" %>

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
                    <td colspan="2" style="text-align:left;padding-bottom:0px;">
                        <%= DateTime.Now.ToShortDateString() %>&nbsp;<%= DateTime.Now.ToLongTimeString() %>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
