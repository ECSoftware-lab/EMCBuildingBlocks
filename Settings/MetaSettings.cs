namespace EMC.BuildingBlocks.Settings
{
    public class MetaSettings
    {
        public string AppId { get; set; } = "";
        public string AppSecret { get; set; } = "";
        public string RedirectUri { get; set; } = "";
        public string ApiVersion { get; set; } = "v25.0";
        public string Ambiente { get; set; } = "dev";  // viene de appsettings

        public string AuthBaseUrl =>
            $"https://www.facebook.com/{ApiVersion}/dialog/oauth";
        public string TokenUrl =>
            $"https://graph.facebook.com/{ApiVersion}/oauth/access_token";
    }
}
