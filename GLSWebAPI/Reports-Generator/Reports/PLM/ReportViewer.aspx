<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.Reports.PLM.ReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        td, th {
            border: solid 1px #e6e6e6;
            text-align: center;
            padding: 5px;
            font-size:18px;
            font-family: 'Times New Roman';
        }

        .footer-bg {
            /*background: url("/img/report-rounded-footer.jpg");
            background-repeat: no-repeat;
            background-size: 100% 160px;*/
            border-radius: 25px;
            border: solid 2px #cccccc;
        }

        .jobnumber-bg {
            /*background: url("/img/report-rounded-footer.jpg");
            background-repeat: no-repeat;*/
            border-radius: 25px;
            border: solid 2px #cccccc;
            text-align: left;
        }
    </style>

</head>
<body style="font-family: 'Times New Roman'; font-size:20px;">
    <form id="form1" runat="server" style="font-family: 'Times New Roman'; line-height: 20px;">
        <%--<div class="jobnumber-bg" style="width:100%;">
            <div style="padding:5px;">
           <b>Job Name:</b> <%= this.project.JobNumber %></div>
        </div>
        <div style="text-align:center; font-weight: bold; width:100%;margin-top: 5px;">
            ASBESTOS PLM REPORT:EPA-600/M4-82-020 & EPA METHOD 600/R-93-116
        </div>
        <div style="text-align:center; width:100%;margin-top: 5px;">
            Detection Limit: Less than 1% by area.
        </div>--%>
        <%--<div style="text-align:right; width:100%;margin-top: 5px;">
            Sampling Date: <%= ((DateTime)this.project?.SamplingDate).ToShortDateString()%><br />
            Date Analyzed: <%= ((DateTime)this.project?.DateOfAnalyzed).ToShortDateString() %>
        </div>--%>
        <div style="text-align: right; width: 100%;">
            <asp:ListView ID="lvSample" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
                <LayoutTemplate>
                    <div style="padding: 20px; font-family: 'Times New Roman'; font-size: 11pt; line-height: 18px;">
                        <table border="0" style="border: none; width: 100%;">
                            <%--<thead>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolderHeader" />
                            </thead>--%>

                            <tbody>
                                <tr>
                                    <td style="border: none;" colspan="2">
                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                    </td>
                                </tr>
                            </tbody>
                           <%-- <tfoot>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolderTest" />
                            </tfoot>--%>
                        </table>
                    </div>
                </LayoutTemplate>
                <GroupTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                </GroupTemplate>
                <ItemTemplate>
                    <%--<tr>
                            <td style="padding-left: 6px;">
                                <%# Eval("LabId") %>
                                <div style="padding-top:5px;">Location:  <%# Eval("Location") %></div>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("SampleType") %>
                            </td>
                            
                        </tr>--%>
                    <table border="0" style="width: 100%">
                        <tr valign="top" runat="server" visible="<%# (Container.DataItemIndex == 0? false: (Container.DataItemIndex) %3 == 0) %>" style="page-break-before: always;">
                                <td colspan="2" style="border: none;">
                                </td>
                            </tr>
                        <tr>
                            <td style="border: none; padding-left: 30px; font-weight: bold; text-align:left;">
                                <div style="padding-top: 0px;">Location <%# Eval("LabId").ToString().Split('-')[1].ToString() %>:  <%# Eval("Location") %></div>
                            </td>
                            <td style="border: none; text-align: right; font-weight: bold; text-align:right;">Lab Id: <%# Eval("LabId") %></td>
                        </tr>
                         
                    </table>
                    <asp:ListView ID="ListView1" runat="server" DataSource='<%# Eval("ProjectSampleDetail") %>' ItemPlaceholderID="addressPlaceHolder" OnDataBound="lvGoalsInner_DataBound">
                        <LayoutTemplate>
                            <div style="padding-left: 30px; padding-bottom: 5px;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border: none;">
                                    <asp:PlaceHolder runat="server" ID="addressPlaceHolder" />
                                    <thead>
                                        <tr>
                                            <th style="border-right: none;width:10%;"></th>
                                            <th style="text-align:center;border-left: none;">Sample Layers</th>
                                            <th style="text-align:center">Asbestos Content</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td style="border-right: none;"></td>
                                        <td style="width: 50%;border-left: none;">Sample Composite Homogeneity</td>
                                        <td style="width: 50%;">
                                            <asp:Literal
                                                runat="server"
                                                ID="ltTitle"
                                                Text='<%# DataBinder.Eval((Container.Parent as ListViewDataItem).DataItem, "SampleCompositeHomogeneityText") %>' /></td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: none;"></td>
                                        <td style="width: 50%;border-left: none;">Composite Non-Asbestos Contents</td>
                                        <td style="width: 50%;">
                                            <asp:Literal
                                                runat="server"
                                                ID="ltCompositeNonAsbestosContentsText"
                                                Text='<%# DataBinder.Eval((Container.Parent as ListViewDataItem).DataItem, "CompositeNonAsbestosContentsText") %>' /></td>
                                    </tr>
                                    <asp:HiddenField runat="server" ID="hdnNote" Value='<%# DataBinder.Eval((Container.Parent as ListViewDataItem).DataItem, "Note") %>' />
                                    <asp:PlaceHolder runat="server" ID="notePlaceHolder">
                                        <%--<tr id="trNote" runat="server" style="width: 100%;">
                                        <td colspan="2" style="width: 100%;">Note:
                                            <asp:Literal                                                
                                                runat="server"
                                                ID="ltNote"
                                                Text='<%# DataBinder.Eval((Container.Parent as ListViewDataItem).DataItem, "Note") %>' /> </td>
                                        </tr>--%>
                                    </asp:PlaceHolder>
                                    
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="border: none;" >
                                <td style="border-right: none; text-align:left;">Layer: <%# Container.DataItemIndex+1 %></td>
                                <td style="width: 50%;border-left: none;">
                                    <asp:Label ID="Label4" Text='<%# Eval("SampleType") %>' runat="server" />
                                </td>
                                <td style="width: 50%;">
                                    <asp:Label ID="Label1" Text='<%# Eval("AbsestosPercentageText") %>' runat="server" />
                                </td>
                            </tr>
                           
                        </ItemTemplate>

                    </asp:ListView>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
