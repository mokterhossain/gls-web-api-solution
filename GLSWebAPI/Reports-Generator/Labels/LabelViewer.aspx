<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabelViewer.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.Labels.LabelViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ListView ID="lvLabel" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                <div  style="<%# (Container.DataItemIndex+1 == this.totalSample? "": "page-break-after: always") %>"; padding: 10px; font-family: 'Times New Roman'; ">
                    
                    <table>
                        <tr>
                            <td style="padding-top: 20px;">
                                <img width="250" src="../../img/logo.png" /></td>
                            <td style="padding-left: 20px; font-size: 50px;padding-top:30px;">
                                <div style="padding-bottom: 6px; font-weight: bold; font-size: 62px;"><%# Eval("QCStrTag") %></div>                                
                                <div><%# Eval("LabelDynamicField") %></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </form>
</body>
</html>
