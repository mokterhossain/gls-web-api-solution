<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer.aspx.cs" Inherits="GLSWebAPI.Footer" %>

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
                    <td style=""width:50%; padding-left:10px; text-align:left;border: none;padding-right:30px;padding-left:30px;line-height:10px;">
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding:0px 10px 0px 10px;">
                            <tr>
                                <td style="border-bottom:solid 2px #cccccc;height: 80px;vertical-align:bottom;"><img style="width: 150px;max-height: 80px;" src="<%= labAnalystSignUrl %>" /></td>
                            </tr>
                            <tr>
                                <td style="font-size: 20px;"><%= projectAnalystName %></td>
                            </tr>
                            <tr>
                                <td style="font-size: 20px;">Lab Analyst <%= projectAnalystDiploma %></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:50%; text-align:left;border: none;vertical-align:bottom;padding-left:30px;padding-right:30px; line-height:24px;height: 80px;">                        
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding:0px 10px 0px 10px;height: 100px;">
                            <tr>
                                <td style="border-bottom:solid 2px #cccccc;height: 80px; vertical-align:bottom"><img style="width: 200px;max-height: 80px;" src="<%= labrotaryManagerSignUrl %>" /></td>
                            </tr>
                            <tr>
                                <td style="font-size: 20px;"><%= labrotaryManagerName %></td>
                            </tr>
                            <tr>
                                <td style="font-size: 20px;">Laboratory Manager <%= labrotaryManagerDiploma %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="<%= this.project.ProjectType == "Mold" || this.project.ProjectType == "Mold Tape Lift" ? "width:100%; padding: 0px 10px; text-align:left;border-radius: 25px;border: solid 2px #cccccc;margin-top: 0px;line-height:18px;" : "width:100%; padding:10px; text-align:left;border-radius: 25px;border: solid 2px #cccccc;margin-top: 10px;" %>">
                        <div style="padding:4px;text-align:justify;font-weight:bold;"><%= footerNoteText %></div>
                    </td>
                </tr>
                <tr style="<%= this.project.ProjectType == "PLM" || this.project.ProjectType == "PCM" ? "" : "display: none" %>">
                    <td colspan="2" style="width: 100%;text-align: right;border:none; font-weight: bold; font-style: italic;">
                        <div style="text-align: right;"><%= SampledBy %></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;padding-bottom:-3px;">
                        <%= DateTime.Now.ToShortDateString() %>&nbsp;<%= DateTime.Now.ToLongTimeString() %>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
