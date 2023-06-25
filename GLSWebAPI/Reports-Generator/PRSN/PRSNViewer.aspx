<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRSNViewer.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.PRSN.PRSNViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .sampleList th, .sampleList td {
            border-bottom:solid 1px #e6e6e6;
            text-align: left;
            padding:3px;
            font-size: 17px;
        }
        .summary td {
            font-size: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="font-weight: bold;font-family: 'Times New Roman'; width:100%; text-align: center;font-size: 22px;">PROJECT SAMPLE RECEIPT NOTIFICATION</div>
            <table class="summary" border="0" style=" padding: 10px;  font-family: 'Times New Roman'; font-size:17px; width: 100%">
                <tr>
                    <td style="width:30%; text-align: right;">Client Name:</td>
                    <td id="tdClientName" runat="server"><%= this.project.ClientName %></td>
                </tr>
                <tr>
                    <td style="width:30%; text-align: right;">Client:</td>
                    <td id="tdClient" runat="server"><%= this.project.ReportingPerson %></td>
                </tr>
                <tr>
                    <td style="width:30%; text-align: right;">Client Job Number:</td>
                    <td id="tdClientJobNumber" runat="server"><%= this.project.JobNumber %></td>
                </tr>
                <tr>
                    <td style="width:30%; text-align: right;">Contact:</td>
                    <td id="tdContact" runat="server"><%= string.Format("{0} {1}", project.CellNo, project.OfficePhone) %></td>
                </tr>
                <tr>
                    <td style="width:30%; text-align: right;">Comment:</td>
                    <td id="tdComment" runat="server"><%= this.project.Comments %></td>
                </tr>
                <tr>
                    <td style="width:30%; text-align: right;">Number Of Sample:</td>
                    <td id="tdNumberOfSample" runat="server"><%= this.project.NumberOfSample %></td>
                </tr>
                
            </table>
            <asp:ListView ID="lvSample" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
            <LayoutTemplate>
                <div style=" padding: 10px;  font-family: 'Times New Roman'; font-size:17px;">
                <table class="sampleList" cellpadding="0" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                 <th style="width:25%;">Lab Id</th>
                                 <th style="width:10%;">Description</th>
                                 <th style="width:6%;">Matrix</th>
                                 <th style="width:15%;">Package Code</th>
                                 <th style="width:14%;">TAT</th>
                                 <th style="width:6%;">QC</th>
                                 <th style="width:24%;">Received/Due Date</th>
                            </tr>
                        </thead>
                        <tbody>
                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                             </tbody>
                    </table>
                    </div>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                
                    
                        <tr>
                            <%--<td>
                                <img width="75" src="../../img/logo.png" /></td>--%>
                            <td style="padding-left: 6px;">
                                <%# Eval("LabId") %>
                                <div style="padding-top:5px;">Location:  <%# Eval("Location") %></div>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("SampleType") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("Matrix") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("PackageCode") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("TurnAroundTime") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("QCStr") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%= this.project.ReceivedDate %>
                                <div style="padding-top:5px;">Due:  <%# Eval("DueDate") %></div>
                            </td>
                            
                        </tr>
            </ItemTemplate>
        </asp:ListView>
        </div>
    </form>
</body>
</html>
