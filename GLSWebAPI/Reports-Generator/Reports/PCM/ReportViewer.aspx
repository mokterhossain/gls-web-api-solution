<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.Reports.PCM.ReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        td, th {
            border: solid 1px #e6e6e6;
            text-align: center;
            padding: 3px;
            font-size: 17px;
        }
        th{
            text-align: center;
        }
        .jobnumber-bg {
            border-radius: 25px;
            border: solid 2px #cccccc;
            text-align: left;
        }
        #footer {
            display: table-footer-group;
            bottom: 0px
        }
     </style>
</head>
<body style="font-family: 'Times New Roman'; font-size:14px;">
    <form id="form1" runat="server">
        <asp:ListView ID="lvPCM" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
            <LayoutTemplate>
                <div style="padding: 20px; font-family: 'Times New Roman'; font-size: 17px;">
                    <div style="font-weight: bold;padding:5px 3px; font-size: 18px;">ASBESTOS AND FIBERS BY PCM: NIOSH 7400 Method - A Rules, Fifth Edition, Issue 3, 14 June 2019</div>
                    <table class="sampleList" cellpadding="0" cellspacing="0" width="100%">
                        <thead style="background-color:#F0E68C;">
                            <tr>
                                <th style="width: 17%;">Lab Id</th>
                                <th style="width: 12%;">Sample Date</th>
                                <th style="width: 13%;">Volume (liters)</th>
                                <th style="width: 15%;">Fiber Detected</th>
                                <th style="width: 12%;">Fields Read</th>
                                <th style="width: 8%;">LOD</th>
                                <th style="width: 10%;">Fibers/mm</th>
                                <th style="width: 15%;">Fibers Per CC</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                        </tbody>
                        <%--<tfoot>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolderFooter" />
                        </tfoot>--%>
                    </table>
                </div>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                <tr style="padding: 10px 5px;">
                    <td colspan="4" style="border:none;font-weight: bold;padding-top: 20px;padding-bottom: 5px;text-align:left;"><%# Eval("LocationGroup") %></td>
                    <td colspan="4" style="border:none;font-weight: bold;padding-top: 20px;padding-bottom: 5px;text-align:left;"><%# Eval("LabIdGroup") %></td>
                </tr>
                <tr style="<%# Eval("Comment") == "" || Eval("Comment") == null ? "" : "display: none;" %> ">
                    <td style="padding-left: 6px; text-align:left;">
                        <%# Eval("LabId") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Eval("SampleDate") == null ? "" : ((DateTime)Eval("SampleDate")).ToShortDateString() %>
                       <%-- <%# ((DateTime)Eval("SampleDate")).ToShortDateString() %>--%>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Convert.ToBoolean(Eval("IsBlank")) == true? "0.00" : Eval("VolumeL") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Convert.ToBoolean(Eval("IsBlank")) == true? "< 5.5" : Eval("CalculatedFiberCount") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Eval("FieldsCounted") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Eval("LOD") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Eval("ReportedFiberPermm") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Eval("ReportedFiberPercc") %>
                    </td>
                </tr>
                <tr style="<%# Eval("Comment") == ""  || Eval("Comment") == null ? "display: none;" : "" %> ">
                    <td style="padding-left: 6px; text-align:left;">
                        <%# Eval("LabId") %>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Eval("SampleDate") == null ? "" : ((DateTime)Eval("SampleDate")).ToShortDateString() %>
                       <%-- <%# ((DateTime)Eval("SampleDate")).ToShortDateString() %>--%>
                    </td>
                    <td style="padding-left: 6px;">
                        <%# Convert.ToBoolean(Eval("IsBlank")) == true? "0.00" : Eval("VolumeL") %>
                    </td>
                    <td colspan="5">
                       <i> <%# Eval("Comment") %></i>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </form>
</body>
</html>
