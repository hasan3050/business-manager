<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LedgerReportViewer.aspx.cs" Inherits="TrialBusinessManager.Web.Report.ReportViewers.LedgerReportViewer" %>

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
        .style2
        {
            width: 53px;
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
                <td>
        <asp:Label ID="RegionLbl" runat="server" Text="Select Region :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="RegionDD" runat="server" DataSourceID="RegionDataSource" 
            DataTextField="RegionName" DataValueField="RegionId" Height="24px" 
            Width="199px" AppendDataBoundItems="True">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table class="style1">
            <tr>
                <td class="style2">
                    From :</td>
                <td>
        <asp:TextBox ID="FromDate" runat="server" Width="138px" 
            style="margin-left: 0px" Height="24px"></asp:TextBox>
        <asp:CalendarExtender ID="FromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
            TargetControlID="FromDate">
        </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    To :</td>
                <td>
                    <asp:TextBox ID="ToDate" 
            runat="server"  Width="136px" style="margin-left: 0px" Height="24px" 
                        ontextchanged="ToDate_TextChanged"></asp:TextBox>
        <asp:CalendarExtender ID="ToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
            TargetControlID="ToDate">
        </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="ViewReportBtn" runat="server" onclick="ViewReportBtn_Click" 
            style="margin-left: 2px; font-family: 'Trebuchet MS';" Text="View Report" 
            Width="140px" Height="31px" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="RegionDataSource" runat="server" 
            SelectMethod="GetRegionsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        &nbsp;</div>
    <rsweb:ReportViewer ID="LedgerReport" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="372px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="831px" 
        style="font-family: 'Trebuchet MS'">
        <LocalReport ReportPath="Report\Designs\LedgerReportDesign.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="LedgerDataSource" Name="LedgerDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="LedgerDataSource" runat="server" 
        SelectMethod="GetReportLedgers" 
        TypeName="TrialBusinessManager.Web.AgroDomainService">
        <SelectParameters>
            <asp:ControlParameter ControlID="RegionDD" Name="RegionId" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="FromDate" Name="StartDate" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="ToDate" Name="EndDate" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
