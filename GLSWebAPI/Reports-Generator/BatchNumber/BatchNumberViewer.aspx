<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchNumberViewer.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.BatchNumber.BatchNumberViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .sampleList th, .sampleList td {
            border-bottom:solid 1px #e6e6e6;
            text-align: center;
            padding:3px;
            font-size: 17px;
        }
        .summary td {
            font-size: 17px;
        }
        thead {display: table-header-group;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListView ID="lvSample" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
            <LayoutTemplate>
                <div style=" padding: 20px;  font-family: 'Times New Roman'; font-size:17px;">
                <table class="sampleList" cellpadding="0" cellspacing="0" width="100%; padding: 10px;">
                        <thead>
                            <tr>
                                <th style="width:10%;">Serial No</th>
                                 <th style="width:15%;">Lab Id</th>
                                 <th style="width:15%;">Matrix</th>
                                 <th style="width:15%;">QC</th>
                                 <th style="width:15%;">Batch #</th>
                                <th style="width:15%;">Project Number</th>
                                <th style="width:15%;">Analyst</th>
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
                
                    <tr valign="top" runat="server" visible="<%# (Container.DataItemIndex == 0? false: (Container.DataItemIndex) %38 == 0) %>" style="page-break-before: always;">
                                <td colspan="2" style="border: none;">
                                </td>
                            </tr>
                        <tr style="<%# (Convert.ToBoolean(Eval("IsQC")) == true? "font-weight: bold;": "") %>">
                            <td style="padding-left: 6px;">
                                <%# (Container.DataItemIndex+1) %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("LabId") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("Matrix") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# (Convert.ToBoolean(Eval("IsQC")) == true? "Yes": "No") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("BatchNumber") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("ProjectNumber") %>
                            </td>
                            <td style="padding-left: 6px;">
                                <%# Eval("AnalystName") %>
                            </td>
                            
                        </tr>
            </ItemTemplate>
        </asp:ListView>
        </div>
    </form>
</body>
</html>
