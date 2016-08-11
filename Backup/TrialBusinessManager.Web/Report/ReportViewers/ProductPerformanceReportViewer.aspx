<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPerformanceReportViewer.aspx.cs" Inherits="TrialBusinessManager.Web.Report.ReportViewers.ProductPerformanceReportViewer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: 'Trebuchet MS'">
    
        <asp:ScriptManager ID="MyScriptManager" runat="server">
        </asp:ScriptManager>
        <br />
    
        Region<br />
        <asp:DropDownList ID="RegionDD" runat="server" AppendDataBoundItems="True" 
            DataSourceID="RegionDataSource" DataTextField="RegionName" 
            DataValueField="RegionId" Height="25px" Width="180px">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
        <asp:ObjectDataSource ID="RegionDataSource" runat="server" 
            SelectMethod="GetRegionsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        <br />
        From :<br />
        <asp:TextBox ID="FromDate" runat="server" Height="25px" Width="173px"></asp:TextBox>
        <asp:CalendarExtender ID="FromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
            TargetControlID="FromDate">
        </asp:CalendarExtender>
        <br />
        To :<br />
        <asp:TextBox ID="ToDate" runat="server" Height="25px" Width="172px"></asp:TextBox>
        <asp:CalendarExtender ID="ToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
            TargetControlID="ToDate">
        </asp:CalendarExtender>
    
        <br />
        <asp:Button ID="Button1" runat="server" Height="30px" onclick="Button1_Click" 
            Text="View report" Width="179px" />
    
    </div>
    <rsweb:ReportViewer ID="ProductPerformanceReport" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="795px">
        <LocalReport ReportPath="Report\Designs\ProductPerformanceReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ProductPerformanceDataSource" 
                    Name="ProductPerformanceDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ProductPerformanceDataSource" runat="server" 
        SelectMethod="CalculateProductPerformance" 
        TypeName="TrialBusinessManager.Web.AgroDomainService">
        <SelectParameters>
            <asp:ControlParameter ControlID="RegionDD" Name="RegionId" 
                PropertyName="SelectedValue" Type="Int64" />
            <asp:ControlParameter ControlID="FromDate" Name="StartDate" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="ToDate" Name="EndDate" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
