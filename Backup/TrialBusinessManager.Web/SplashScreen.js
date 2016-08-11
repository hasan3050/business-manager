function onSourceDownloadProgressChanged(sender, eventArgs) 
{
    sender.findName("uxStatus").Text = Math.round((eventArgs.progress * 100), 0).toString()+" %";   

}