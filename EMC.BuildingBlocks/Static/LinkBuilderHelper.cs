namespace EMC.BuildingBlocks.Static
{
    public static class LinkBuilderHelper
    {
        public static string BuildUrl(
            string schema,
            string host,
            string controller,
            string action,
            Dictionary<string, string>? queryParams = null)
        {
            if (string.IsNullOrWhiteSpace(schema))
                throw new ArgumentException("El esquema no puede ser nulo o vacío.", nameof(schema));

            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentException("El host no puede ser nulo o vacío.", nameof(host));

            var hostParts = host.Split(':');
            var hostname = hostParts[0]; // "localhost"
            var port = hostParts.Length > 1 ? int.Parse(hostParts[1]) : -1; // "8200" (si está presente)

            var uriBuilder = new UriBuilder
            {
                Scheme = schema,
                Host = hostname,
                Port = port,
                Path = $"{controller}/{action}"
            };

            if (queryParams != null && queryParams.Any())
            {
                uriBuilder.Query = string.Join("&", queryParams.Select(kvp =>
                    $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            }

            return uriBuilder.ToString();
        }

        // Extra: método específico para recuperación de contraseña
        public static string BuildPasswordRecoveryUrl(string schema, string host, string userId, string token)
        {

            return BuildUrl(schema, host, "Access", "ResetPassword", new Dictionary<string, string>
            {
                { "userid", userId },
                { "cadena", token }
            });
        }

        // Extra: método específico para confirmación de correo
        public static string BuildEmailConfirmationUrl(string schema, string host, string userId, string token)
        {
            return BuildUrl(schema, host, "Access", "ConfirmEmail", new Dictionary<string, string>
            {
                { "userid", userId },
                { "cadena", token }
            });
        }
    }
}
