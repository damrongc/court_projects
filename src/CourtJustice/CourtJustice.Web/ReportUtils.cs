namespace CourtJustice.Web
{
    public static class ReportUtils
    {
        public static string DesignerPath(IWebHostEnvironment _webHost)
        {
            return Path.Combine(_webHost.WebRootPath, "reports");
        }
        public static string ExportPath(IWebHostEnvironment _webHost)
        {
            return Path.Combine(_webHost.WebRootPath, "notices");
        }
    }
}
