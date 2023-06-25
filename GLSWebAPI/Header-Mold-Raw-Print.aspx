<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header-Mold-Raw-Print.aspx.cs" Inherits="GLSWebAPI.Header_Mold_Raw_Print" %>

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
                    <td style="width:15%; padding-left:10px;">
                        <img width="100" src="img/logo.png" />
                    </td>
                    <td style="width:50%; line-height: 30px; padding-top: 10px; text-align:left">
                        <div>GUARDIAN LAB SERVICES</div>
                        <div>11-2280 39 AVE NE</div>
                        <div>CALGARY, AB. T2E 6P7</div>
                        <div>PHONE:(403) 452-1003</div>
                    </td>
                    <td rowspan="2"  style="width:35%;">
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding:5px 5px 5px 10px;font-size: 18px;">
                            <tr>
                                <td>Analyzed By:</td>
                                <td colspan="2"><%= this.project.ProjectAnalystName %></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height:30px;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Analyzed Date:</td>
                                <td><%= Convert.ToDateTime(this.project?.DateOfAnalyzed).ToShortDateString() %></td>
                            </tr>
                            <tr>
                                <td>Project Number:</td>
                                <td><%= this.project.ProjectNumber %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style=""font-size: 16px;">
                        <div style="padding-left: 14px;">e-mail: customerservice@guardianlab.ca</div>
                    </td>
                </tr>
            </table>
            <div style="width:100%;border-radius: 25px;border: solid 2px #cccccc;margin-top:4px;"><div style="padding:3px 3px 3px 10px;"><b>Job Name:</b> <%= this.project.JobNumber %></div></div>
            <div style="text-align:center; font-weight: bold; width: 100%; margin-top: 5px;" runat="server" id="dvPlmTitle1"><span style="padding-bottom: 0.8px;border-bottom-style: solid;border-bottom-width: 1.1px;width: fit-content;">ASBESTOS PLM REPORT:EPA-600/M4-82-020 & EPA METHOD 600/R-93-116</span></div>
            <div style="text-align:center; width: 100%; margin-top: 5px;" runat="server" id="dvPlmTitle2"><span style="padding-bottom: 0px;border-bottom-style: solid;border-bottom-width: 1.1px;width: fit-content;">Detection Limit: Less than 1% by area.</span></div>
            <table cellpadding="0" cellspacing="0" width="100%" border="0" style="margin-top:2px;">
                <tr>
                    <td style="width:60%">
                        <div style="<%= this.project?.ProjectType == "Mold"? "text-align: right;" : "display:none;" %>">
                            <b>Spore Trap Analysis Report</b>
                        </div>
                        <div style="<%= this.project?.ProjectType == "Mold Tape Lift"? "text-align: right;" : "display:none;" %>">
                            <b>Mold Tape Lift Analysis Report</b>
                        </div>
                    </td>
                    <td style="width: 26%; text-align: right">Sampling Date :&nbsp;</td>
                    <td style="text-align: left;width: 14%;"><%= (this.project?.SamplingDate != null? ((DateTime)this.project?.SamplingDate).ToShortDateString() : "")%></td>
                </tr>
                <tr runat="server" id="trDateAnalyzed">
                    <td style="width:60%"></td>
                    <td style="width: 26%; text-align: right; padding-top:3px;">Date Analyzed :&nbsp;</td>
                    <td style="text-align: left;width: 14%;"><%= (this.project?.DateOfAnalyzed != null? ((DateTime)this.project?.DateOfAnalyzed).ToShortDateString(): "")%></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
