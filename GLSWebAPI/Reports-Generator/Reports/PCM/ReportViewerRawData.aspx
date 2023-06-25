<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerRawData.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.Reports.PCM.ReportViewerRawData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .sampleList th, .sampleList td {
            border:solid 1px #e6e6e6;
            text-align: center;
            padding:0px;
            font-size: 14px;

        }
        .summary td {
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: 'Times New Roman';">
            <div style="float:left; width: 60%">
             <asp:ListView ID="lvRawBlankData" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
            <LayoutTemplate>
                <div style=" padding: 0px 10px;  font-family: 'Times New Roman'; font-size:17px;">
                <table class="sampleList" cellpadding="0" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                 <th rowspan="3" style="background-color: #F5F5F5"></th>
                                 <th colspan="4">Field Blank Raw Data</th>
                                 <th rowspan="2" style="background-color: #F5F5F5">Calculated Fibers</th>
                            </tr>
                            <tr>
                                <th rowspan="2">GLS Sample</th>
                                <th colspan="2">Raw Fiber Counts </th>
                                <th></th>
                            </tr>
                            <tr>
                                <th>1/2</th>
                                <th>Whole</th>
                                <th>Fields Counted</th>
                                <th style="background-color: #F5F5F5">(count)</th>
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
                            <td style="background-color: #F5F5F5"><%# Eval("Serial") %></td>
                            <td><%# Eval("BCSample") %></td>
                            <td><%# Eval("RawFibersCountHalf") %></td>
                            <td><%# Eval("RawFibersCountWhole") %></td>
                            <td><%# Eval("FiledsCounted") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("CalculatedFibersCount") %></td>
                        </tr>
            </ItemTemplate>
        </asp:ListView>
                </div>
            <div style="float:left; width: 40%; text-align: left; line-height: 30px;">
                <div style="padding-left: 100px; padding-top: 30px;">
                Project Number: <%= this.projectNumber %><br />
                Analyzed By: <%= this.projectAnalystName %><br />
                Analyzed Date: <%= this.dateOfAnalyzed %>
                    </div>
            </div>
            <div style="clear: both; width: 100%;">
                <asp:ListView ID="lvPCM" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
            <LayoutTemplate>
                <div style=" padding: 2px 10px;  font-family: 'Times New Roman'; font-size:17px;">
                <table class="sampleList" cellpadding="0" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                 <th rowspan="3" style="background-color: #F5F5F5"></th>
                                <th rowspan="3">GLS Sample</th>
                                <th rowspan="3">Sample Date</th>
                                 <th colspan="5">Raw Data</th>
                                 <th colspan="3" style="background-color: #F5F5F5">Calculated Values</th>
                                <th colspan="3" style="background-color: #F5F5F5">Reported</th>
                            </tr>
                            <tr>
                                <th colspan="2">Raw Fiber Count</th>
                                <th rowspan="2">Fields Counted </th>
                                <th rowspan="2">Volume(L) </th>
                                <th rowspan="2">Filter Area (mm²) </th>
                                <th colspan="3" style="background-color: #F5F5F5">Fibers</th>
                                <th colspan="3" style="background-color: #F5F5F5">Fibers</th>

                            </tr>
                            <tr>
                                <th>1/2</th>
                                <th>Whole</th>
                                <th style="background-color: #F5F5F5">(count)</th>
                                <th style="background-color: #F5F5F5">(per mm²)</th>
                                <th style="background-color: #F5F5F5">(per cc)</th>
                                <th style="background-color: #F5F5F5">(per mm²)</th>
                                <th style="background-color: #F5F5F5">(per cc)</th>
                                <th style="background-color: #F5F5F5">LOD</th>
                            </tr>
                        </thead>
                        <tbody>
                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                             </tbody>
                    <tfoot>
                        
                    </tfoot>
                    </table>
                    </div>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1">

                </asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                
                    
                        <tr>
                            <td style="background-color: #F5F5F5"><%# Convert.ToBoolean(Eval("IsDuplicate")) == true ? "Dup" : Convert.ToString((Container.DataItemIndex + 1)) %></td>
                            <td><%# Eval("BCSample") %></td>
                            <td><%# Eval("SampleDate") != null ? Convert.ToDateTime(Eval("SampleDate")).ToShortDateString() : "" %></td>
                            <td><%# Eval("RawFiberCountHalf") %></td>
                            <td><%# Eval("RawFiberCountWhole") %></td>
                            <td><%# Eval("FieldsCounted") %></td>
                            <td><%# Eval("VolumeL") %></td>
                            <td><%# Eval("FilterAreaMM") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("CalculatedFiberCount") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("CalculatedFiberPermm") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("CalculatedFiberPercc") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("ReportedFiberPermm") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("ReportedFiberPercc") %></td>
                            <td style="background-color: #F5F5F5"><%# Eval("LOD") %></td>
                        </tr>
            </ItemTemplate>
        </asp:ListView>
                <div style=" padding: 0px 10px;  font-family: 'Times New Roman'; font-size:17px;">
                <table  class="sampleList" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                            <td style="background-color: #F5F5F5; border-right:none;width:30px;">CV</td>
                            <td style="border-right:none; border-left: none;">Original:</td>
                            <td style="border-right:none; border-left: none;"><%= this.originalValue %></td>
                            <td style="border-right:none; border-left: none;">Duplicate:</td>
                            <td style="border-right:none; border-left: none;"><%= this.duplicateValue %></td>
                            <td style="border-right:none; border-left: none;">TV= </td>
                            <td style="border-right:none; border-left: none;"><%= this.tvValue %></td>
                            <td style="border-right:none; border-left: none;">|Diff|= </td>
                            <td style="border-right:none; border-left: none;"><%= this.difValue %></td>
                            <td style="text-align:left;border-right:none; border-left: none; <%= this.qcResult == "Pass"? "background-color:yellowgreen;": this.qcResult == "Fail"? "background-color:red;" : "" %>">Result: </td>
                            <td style="text-align:left;border-left: none;<%= this.qcResult == "Pass"? "background-color:yellowgreen;":  this.qcResult == "Fail"? "background-color:red;" : "" %>"><%= this.qcResult %></td>
                        </tr>
                        <tr>
                            <td colspan="11" style="text-align:left;">
                                <img src="../../../img/pcm-formula.jpg" />
                            </td>
                        </tr>
                </table>
                    </div>
            </div>
        </div>
    </form>
</body>
</html>
