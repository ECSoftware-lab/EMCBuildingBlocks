namespace EMC.BuildingBlocks.Errors
{
    public static class ErrorCodes
    {
        public static ErrorDefinition GetCode(int code)
        {
            return Messages.FirstOrDefault(e => e.Code == code)
                ?? throw new Exception($"No se encontró un error con el código {code}");
        }
        public static ErrorDefinition GetCode(int code, string extraMessage)
        {
            var definition = GetCode(code);
            return new ErrorDefinition
            {
                Code = definition.Code,
                Message = $"{definition.Message} {extraMessage}"
            };
        }
        public static class Codes
        {
            public const int AlreadyReported = 100000;
            public const int FieldNotValid = 100036;
            public const int FieldNull = 100037;
            public const int FieldDuplicate = 100038;
            public const int DontMatch = 100039;
            public const int UncreatedResource = 100040;
            public const int LoguinError = 100041;
            public const int CriticanlError = 100042;
            public const int NotFound = 100043;
            public const int UserNotConfirm = 100044;
            public const int FieldMissing = 100045;
            public const int UnupdatedResource = 100046;
        }

        public static readonly List<ErrorDefinition> Messages =
        [
            new() { Code = Codes.AlreadyReported, Message = "Ya Implementado." },
        new() { Code = Codes.FieldNotValid, Message = "El valor del campo no es valido." },
        new() { Code = Codes.FieldNull, Message = "El campo es obligatorio." },
        new() { Code = Codes.FieldDuplicate, Message = "Error de duplicación." },
        new() { Code = Codes.FieldMissing, Message = "Error de valor requerido." },
        new() { Code = Codes.DontMatch, Message = "No coinciden." },
        new() { Code = Codes.UncreatedResource, Message = "El recurso no se creó." },
        new() { Code = Codes.LoguinError, Message = "No pudo acceder." },
        new() { Code = Codes.CriticanlError, Message = "Error crítico." },
        new() { Code = Codes.NotFound, Message = "Recurso no encontrado." },
        new() { Code = Codes.UserNotConfirm, Message = "Email de usuario no confirmado." },
            new () { Code = Codes.UnupdatedResource, Message = "El recurso no se actualizó." }
        ];
    }

}
