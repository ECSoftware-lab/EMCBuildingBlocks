namespace EMC.BuildingBlocks.Static
{
    public static class TimeZoneHelper
    {
        public static string NormalizarZonaHoraria(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "UTC"; // fallback seguro

            var normalized = input.Trim().ToUpperInvariant();

            return normalized switch
            {
                // Argentina
                "ARGENTINA" or "AR" => "America/Argentina/Buenos_Aires",

                // Países limítrofes
                "CHILE" or "CL" => "America/Santiago",
                "URUGUAY" or "UY" => "America/Montevideo",
                "PARAGUAY" or "PY" => "America/Asuncion",
                "BOLIVIA" or "BO" => "America/La_Paz",
                "BRASIL" or "BRAZIL" or "BR" => "America/Sao_Paulo",

                // Otros países hispanohablantes (ejemplos)
                "ESPAÑA" or "SPAIN" or "ES" => "Europe/Madrid",
                "ANDORRA" or "AD" => "Europe/Andorra",
                "MEXICO" or "MÉXICO" or "MX" => "America/Mexico_City",
                "COLOMBIA" or "CO" => "America/Bogota",
                "PERU" or "PERÚ" or "PE" => "America/Lima",
                "ECUADOR" or "EC" => "America/Guayaquil",
                "VENEZUELA" or "VE" => "America/Caracas",

                // Fallback: devolver tal cual
                _ => input
            };
        }
    }

}
