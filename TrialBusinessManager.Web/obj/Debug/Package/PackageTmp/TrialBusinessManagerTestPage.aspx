<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <!--Custom splash script-->
	<!--<script type="text/javascript" src="splashscreen.js"></script>-->
    <!--
    <script id="xamlSplash" type="text/xaml">
    <Grid x:Name="LayoutRoot" 
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"       
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" 
	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	mc:Ignorable="d"
	xmlns:sdk="http://schemas.microsoft.com/winfx/2006/xaml/presentation/sdk">
		<TextBlock Margin="247,196,261,238" TextWrapping="Wrap" Text="anviLab" TextAlignment="Center" FontSize="32">
			<TextBlock.Foreground>
				<LinearGradientBrush EndPoint="0.5,1" StartPoint="0.5,0">
					<GradientStop Color="#FF2BB7E5" Offset="0"/>
					<GradientStop Color="#FF0E5C76" Offset="1"/>
				</LinearGradientBrush>
			</TextBlock.Foreground>
		</TextBlock>
		<TextBlock HorizontalAlignment="Right" Height="18" Margin="0,198,250,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="23"><Span FontSize="17.3333333333333" FontFamily="Arial"><Run Text="®"/></Span></TextBlock>
		<TextBlock Margin="135,224,148,217" TextWrapping="Wrap" Text="BUSINESS MANAGER" TextAlignment="Center" FontSize="32">
			<TextBlock.Foreground>
				<SolidColorBrush Color="{StaticResource Gray13}"/>
			</TextBlock.Foreground>
		</TextBlock>
		<TextBlock x:Name="uxStatus" Margin="280,0,305,185" TextWrapping="Wrap" Text="0 %" FontWeight="Bold" FontSize="18.667" TextAlignment="Center" Height="28" VerticalAlignment="Bottom">
			<TextBlock.Foreground>
				<SolidColorBrush Color="{StaticResource ChartBrush3}"/>
			</TextBlock.Foreground>
		</TextBlock>
	</Grid>
 
    </script>
    -->
    <title>TrialBusinessManager</title>
    <style type="text/css">
    html, body {
	    height: 100%;
	    overflow: auto;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }
    </style>
    <script type="text/javascript" src="Silverlight.js"></script>
	
    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
              appSource = sender.getHost().Source;
            }
            
            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
              return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " +  appSource + "\n" ;

            errMsg += "Code: "+ iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {           
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " +  args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="height:100%">
    <div id="silverlightControlHost">
	
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
		  <param name="source" value="ClientBin/B.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="4.0.60310.0" />
		  <param name="autoUpgrade" value="true" />
		  <!--Custom splash settings
           <param name="splashscreensource" value="#xamlSplash"/>
           <param name="onSourceDownloadProgressChanged" value="onSourceDownloadProgressChanged" />
           -->
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.60310.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
    </form>
</body>
</html>
