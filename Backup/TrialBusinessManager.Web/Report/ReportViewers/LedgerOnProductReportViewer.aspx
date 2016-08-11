<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LedgerOnProductReportViewer.aspx.cs" Inherits="TrialBusinessManager.Web.Report.ReportViewers.LedgerOnProductReportViewer" %>

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
            width: 50px;
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
            DataTextField="RegionName" DataValueField="RegionId" Height="26px" 
            Width="195px" AppendDataBoundItems="True">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Select Product :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" 
            DataSourceID="ProductDataSource" DataTextField="ProductName" 
            DataValueField="ProductId" Width="196px" Height="23px">
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
        <asp:TextBox ID="FromDate" runat="server" Width="135px" Height="19px" 
            style="margin-left: 0px"></asp:TextBox>
        
        
        
        
        <asp:CalendarExtender ID="FromDate_CalendarExtender" runat="server"  Format="dd-MM-yyyy"
            TargetControlID="FromDate">
        </asp:CalendarExtender>
        
        
        
        
                </td>
            </tr>
            <tr>
                <td class="style2">
                    To :</td>
                <td>
                    <asp:TextBox ID="ToDate" 
            runat="server"  Width="135px" Height="19px"  style="margin-left: 0px" 
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
            Width="139px" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="RegionDataSource" runat="server" 
            SelectMethod="GetRegionsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        &nbsp;<asp:ObjectDataSource ID="ProductDataSource" runat="server" 
            SelectMethod="GetProductsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        &nbsp;&nbsp;<br />
    </div>
    <rsweb:ReportViewer ID="LedgerOnProductReport" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="820px">
        <LocalReport ReportPath="Report\Designs\ProductLedgerReportDesign.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="LedgerOnProductReportDataSource" 
                    Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="LedgerOnProductReportDataSource" runat="server" 
        SelectMethod="GetReportLedgersOnProducts" 
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
    </form>
</body>
</html>
