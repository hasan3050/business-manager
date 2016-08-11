<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerCollectionReportViewer.aspx.cs" Inherits="TrialBusinessManager.Web.Report.ReportViewers.DealerCollectionReportViewer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 31px;
        }
        .style2
        {
            width: 42px;
        }
        .style3
        {
            width: 122px;
        }
        .style4
        {
            width: 125px;
        }
        .style5
        {
            width: 122px;
            height: 32px;
        }
        .style6
        {
            height: 32px;
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
                <td class="style5">
        <asp:Label ID="Label2" runat="server" Text="Select Region :"></asp:Label>
                &nbsp; 
                </td>
                <td class="style6">
                    <asp:DropDownList ID="RegionDD" runat="server" DataSourceID="RegionDataSource" 
            DataTextField="RegionName" DataValueField="RegionId" Height="27px" 
            Width="204px" 
                        onselectedindexchanged="RegionDD_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style5">
        <asp:Label ID="RegionLbl" runat="server" Text="Select Dealer :"></asp:Label>
                &nbsp; 
                </td>
                <td class="style6">
                    <asp:DropDownList ID="DealerDD" runat="server" Height="27px" 
            Width="204px" AppendDataBoundItems="True" 
                        DataSourceID="DealerDataSource" DataTextField="CompanyName" 
                        DataValueField="DealerId">
            <asp:ListItem Value="">--All--</asp:ListItem>
        </asp:DropDownList>
                    <asp:ObjectDataSource ID="DealerDataSource" runat="server" 
                        SelectMethod="GetDealersForReport" 
                        TypeName="TrialBusinessManager.Web.AgroDomainService">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RegionDD" Name="RegionId" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
     
            
           
        </table>
        <table class="style1">
            <tr>
                <td class="style2">
                    From </td>
                <td class="style4">
        <asp:TextBox ID="FromDate" runat="server" Width="120px" Height="20px" 
            style="margin-left: 0px"></asp:TextBox>
        
        
        
        
        <asp:CalendarExtender ID="FromDate_CalendarExtender" runat="server"  Format="dd-MM-yyyy"
            TargetControlID="FromDate">
        </asp:CalendarExtender>
        
        
     
        
                </td>
                <td>
                    To
                    <asp:TextBox ID="ToDate" 
            runat="server"  Width="130px" Height="20px"  style="margin-left: 0px" 
                        ontextchanged="ToDate_TextChanged"></asp:TextBox>
        
        
        
        <asp:CalendarExtender ID="ToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
            TargetControlID="ToDate">
        </asp:CalendarExtender>
        
        
        
        
                </td>
            </tr>
            </table>
                    <asp:Button ID="ViewReportBtn" runat="server" onclick="ViewReportBtn_Click" 
            style="margin-left: 2px; font-family: 'Trebuchet MS';" Text="View Report" 
            Width="338px" />
        <asp:ObjectDataSource ID="RegionDataSource" runat="server" 
            SelectMethod="GetRegionsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ProductDataSource" runat="server" 
            SelectMethod="GetProductsForReport" 
            TypeName="TrialBusinessManager.Web.AgroDomainService">
        </asp:ObjectDataSource>
    </div>
    <rsweb:ReportViewer ID="DealerCollectionReport" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" Height="550px" WaitMessageFont-Size="14pt" Width="1100px" 
        >
        <LocalReport ReportPath="Report\Designs\DealerCollectionReportDesign.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="CollectionReportDataSource" 
                    Name="ProductLedgerDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="CollectionReportDataSource" runat="server" 
        SelectMethod="GetCollectionReport" 
        TypeName="TrialBusinessManager.Web.AgroDomainService">
        <SelectParameters>
        <asp:ControlParameter ControlID="DealerDD" Name="DealerId" 
                PropertyName="SelectedValue" Type="Int32" />
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

