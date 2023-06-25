<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer-Mold-Raw-Data.aspx.cs" Inherits="GLSWebAPI.Footer_Mold_Raw_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 0px 20px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style=""width:50%; padding-left:10px; text-align:left;border: none;padding-right:30px;padding-left:30px;line-height:24px;">
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding:0px 10px 1px 10px;">
                            <tr>
                                <td style="border-bottom:solid 2px #cccccc;height: 100px;vertical-align:bottom;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="font-size: 16px;"><%= projectAnalystName %></td>
                            </tr>
                            <tr>
                                <td style="font-size: 16px;">Lab Analyst <%= projectAnalystDiploma %></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:50%; text-align:left;border: none;vertical-align:bottom;padding-left:30px;padding-right:30px; line-height:24px;height: 100px;">                        
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding:0px 10px 1px 10px;height: 100px;">
                            <tr>
                                <td style="border-bottom:solid 2px #cccccc;height: 100px; vertical-align:bottom">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="font-size: 16px;"><%= labrotaryManagerName %></td>
                            </tr>
                            <tr>
                                <td style="font-size: 16px;">Laboratory Manager <%= labrotaryManagerDiploma %></td>
                            </tr>
                        </table>
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
