<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="GLSWebAPI.Reports_Generator.Invoice.ReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table{
            border:solid 1px #D3D3D3;
            font-family: 'Times New Roman';
            font-size: 18px;
        }
        thead tr th{
            padding:10px;
            text-align: center;
            font-family: 'Times New Roman';
            font-size: 18px;
            border:solid 1px #D3D3D3;
        }
        td{
            padding:7px;
            border:solid 1px #D3D3D3;
        }
        .invoice-summary td{
            border:solid 1px #D3D3D3;
        }
        .billto td{
            border:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: left; width: 100%;">
            <div style="padding:0 25px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="border:none; margin-top:15px;" class="billto">
                <tr>
                    <td colspan="2" style="background-color:#6A5ACD; font-weight:bold; color:#ffffff;padding:3px 5px;font-size:22px;">Bill To</td>
                    <td style="padding:3px 5px;text-align:right;">Customer Id:</td>
                    <td style="padding:3px 5px;"><%= this.clientInvoiceData.CustomerID %></td>
                </tr>
                <tr>
                    <td style="width:15%;padding:3px 5px;">Client:</td>
                    <td style="width:45%;padding:3px 5px;"><%= this.project.ReportingPerson %></td>
                    <td style="width:20%;padding:3px 5px;text-align:right;"">Invoice Date:</td>
                    <td style="width:20%;padding:3px 5px;"><%= Convert.ToDateTime(this.clientInvoiceData.InvoiceDate).ToShortDateString() %></td>
                </tr>
                <tr>
                    <td style="width:15%;padding:3px 5px;">Company:</td>
                    <td style="width:45%;padding:3px 5px;"><%= this.project.ClientName %></td>
                    <td style="width:20%;padding:3px 5px;text-align:right;"">Invoice Number:</td>
                    <td style="width:20%;padding:3px 5px;"><%= this.clientInvoiceData.InvoiceNumber %></td>
                </tr>
                <tr>
                    <td style="width:15%;padding:3px 5px;">Address:</td>
                    <td style="width:45%;padding:3px 5px;"><%= this.project.Address %></td>
                    <td style="width:20%;padding:3px 5px;text-align:right;"">Payment Due By:</td>
                    <td style="width:20%;padding:3px 5px;"><%= Convert.ToDateTime(this.clientInvoiceData.PaymentDueDate).ToShortDateString() %></td>
                </tr>
                <tr>
                    <td style="width:15%;padding:3px 5px;">Phone:</td>
                    <td style="width:45%;padding:3px 5px;"><%= this.project.OfficePhone %></td>
                    <td style="width:20%;padding:3px 5px;text-align:right;"">Project Number:</td>
                    <td style="width:20%;padding:3px 5px;"><%= this.project.ProjectNumber %></td>
                </tr>
                <tr>
                    <td style="width:15%;padding:3px 5px;">Email:</td>
                    <td style="width:45%;padding:3px 5px;"><%= this.project.ClientEmail %></td>
                    <td style="width:20%;padding:3px 5px;text-align:right;""></td>
                    <td style="width:20%;padding:3px 5px;"></td>
                </tr>
                <tr>
                    <td style="width:15%;padding:3px 5px;">Job Number:</td>
                    <td style="width:45%;padding:3px 5px;"><%= this.project.JobNumber.Replace(',',' ') %></td>
                    <td style="width:20%;padding:3px 5px;text-align:right;""></td>
                    <td style="width:20%;padding:3px 5px;"></td>
                </tr>
            </table>
                </div>
            <asp:ListView ID="lvClientInvoice" runat="server" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1">
                <LayoutTemplate>
                    
                    <div style="padding: 20px; font-family: 'Times New Roman'; font-size: 11pt; line-height: 18px;">                        
                        <div style="margin-bottom: 0px; padding: 0 6px;">
                            <div style="border-bottom: solid 0.125rem gray; width: 100%;"></div>
                            <div style="border-top: solid 1.4pt gray; width: 100%; margin-top: 3px"></div>
                        </div>
                        <table border="0" style="width: 100%;border:none; ">
                            <%--<thead>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolderHeader" />
                            </thead>--%>

                            <tbody>
                                <tr>
                                    <td style="border: none;">
                                        <%# Eval("InvoiceNumber") %>
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
                    <asp:ListView ID="ListView1" runat="server" DataSource='<%# Eval("ClientInvoiceDetails") %>' ItemPlaceholderID="addressPlaceHolder">
                        <LayoutTemplate>
                            <div style="padding-bottom: 5px;min-height:400px;border:solid 1px #D3D3D3;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; ">
                                    <asp:PlaceHolder runat="server" ID="addressPlaceHolder" />
                                    <thead style="background-color: #6A5ACD; padding:5px; color:#ffffff">
                                        <tr>
                                            <th>Item Code</th>
                                            <th>Sample TAT</th>
                                            <th>Matrix</th>
                                            <th>Qty</th>
                                            <th>Unit Price</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center">
                                    <asp:Label ID="Label4" Text='<%# Eval("ItemCode") %>' runat="server" /></td>
                                <td style="text-align:center">
                                    <asp:Label ID="Label1" Text='<%# Eval("SampleType") %>' runat="server" /></td>
                                <td style="text-align:center">
                                    <asp:Label ID="Label2" Text='<%# Eval("Matrix") %>' runat="server" /></td>
                                <td style="text-align:right">
                                    <asp:Label ID="Label3" Text='<%# Eval("Quantity") %>' runat="server" /></td>
                                <td style="text-align:right">
                                    <asp:Label ID="Label5" Text='<%# string.Format("${0:0.00}",Eval("UnitPrice")) %>' runat="server" /></td>
                                <td style="text-align:right">
                                    <asp:Label ID="Label6" Text='<%# string.Format("${0:0.00}",Eval("TotalAmount")) %>' runat="server" /></td>
                            </tr>

                            <%--<tr style="border: none;">
                                <td style="border-right: none; text-align: left;">Layer: <%# Container.DataItemIndex+1 %></td>
                                <td style="width: 50%; border-left: none;">
                                    <asp:Label ID="Label4" Text='<%# Eval("SampleType") %>' runat="server" />
                                </td>
                                <td style="width: 50%;">
                                    <asp:Label ID="Label1" Text='<%# Eval("AbsestosPercentageText") %>' runat="server" />
                                </td>
                            </tr>--%>
                        </ItemTemplate>

                    </asp:ListView>
                    <table cellpadding="0" cellspacing="0" style="text-align: right; width: 100%; border: none">
                        <tr>
                            <td style="width: 50%; border:none;"></td>
                            <td style="border:none">
                                <table cellpadding="0" cellspacing="0" class="invoice-summary" style="width:100%;border:solid 1px #D3D3D3">
                                    <tr>
                                        <td style="border:none; text-align:left;">Sub Total:</td>
                                        <td>$<%# string.Format("{0:0.00}",Eval("SubTotal")) %> CAD</td>
                                    </tr>
                                    <tr>
                                        <td style="border:none; text-align:left;">Tax Amount:</td>
                                        <td>$<%# string.Format("{0:0.00}",Eval("TaxAmount")) %> CAD</td>
                                    </tr>
                                    <tr>
                                        <td style="border:none; text-align:left;">PST:</td>
                                        <td>$<%# string.Format("{0:0.00}",Eval("PST")) %> CAD</td>
                                    </tr>
                                    <tr>
                                        <td style="border:none; text-align:left;">Shipping, Handling & Others:</td>
                                        <td>$<%# string.Format("{0:0.00}",Eval("Shipping")) %> CAD</td>
                                    </tr>
                                    <tr>
                                        <td style="border:none; text-align:left;">Discount:</td>
                                        <td>$<%# string.Format("{0:0.00}",Eval("Discount")) %> CAD</td>
                                    </tr>
                                    <tr>
                                        <td style="border:none; text-align:left;">Total:</td>
                                        <td>$<%# string.Format("{0:0.00}",Eval("Total")) %> CAD</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>

                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
