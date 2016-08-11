<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryLogReportViewer.aspx.cs" Inherits="TrialBusinessManager.Web.Report.ReportViewers.InventoryLogReportViewer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style7
        {
            width: 116px;
        }
        .style8
        {
            width: 51px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: 'Trebuchet MS'">
    
        <asp:ScriptManager ID="MyScriptManager" runat="server">
        </asp:ScriptManager>
        <table class="style1">
            <tr>
                <td class="style7">
                    <asp:Label ID="Label1" runat="server" Text="Select Product :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style7">
        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" 
            DataSourceID="ProductDataSource" DataTextField="GroupName" 
            DataValueField="ProductId" Width="195px" Height="26px" AutoPostBack="True">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="RegionLbl" runat="server" Text="Select Region :"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="style1">
            <tr>
                <td class="style7">
                    <asp:DropDownList ID="RegionDD" runat="server" DataSourceID="RegionDataSource" 
            DataTextField="RegionName" DataValueField="RegionId" Height="26px" 
            Width="195px" AppendDataBoundItems="True" AutoPostBack="True">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
            </table>

        <table class="style1">
            <tr>
                <td class="style8">
                    <asp:Label ID="Label2" runat="server" Text="From :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="FromDate" runat="server" Height="19px" Width="135px"></asp:TextBox>
                    <asp:CalendarExtender ID="FromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                        Enabled="True" TargetControlID="FromDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="Label3" runat="server" Text="To :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ToDate" runat="server" Height="19px" Width="135px"></asp:TextBox>
                    <asp:CalendarExtender ID="ToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                        Enabled="True" TargetControlID="ToDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="ViewReportBtn" runat="server" Height="32px" 
                        onclick="ViewReportBtn_Click1" Text="View Report" Width="141px" />
                </td>
            </tr>
        </table>

        <asp:ObjectDataSource ID="RegionDataSource" runat="server" 
            SelectMethod="GetRegionsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ProductDataSource" runat="server" 
            SelectMethod="GetProductsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        &nbsp;&nbsp;</div>
    <rsweb:ReportViewer ID="InventoryLogReport" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1100px">
        <LocalReport ReportPath="Report\Designs\InventoryLogReportDesign.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="InventoryLogDataSource" 
                    Name="InventoryLogDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="InventoryLogDataSource" runat="server" 
        SelectMethod="GetInventoryLogsForReport" 
        TypeName="TrialBusinessManager.Web.AgroDomainService">
        <SelectParameters>
            <asp:ControlParameter ControlID="RegionDD" Name="RegionId" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownList1" Name="ProductId" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="FromDate" Name="StartDate" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="ToDate" Name="EndDate" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    </form>
</body>
</html>
