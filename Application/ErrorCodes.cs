namespace EMC.BuildingBlocks.Application
{
    //PRUEBA
    public static class ErrorCodes
    {
        public static ApiError GetCode(int code)
        {
            return Messages.FirstOrDefault(e => e.Code == code)
                ?? throw new Exception($"No se encontró un error con el código {code}");
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
        };

        public static List<ApiError> Messages = new List<ApiError>
        {new ApiError{ Code = Codes.AlreadyReported, Message = "Ya Implementado. " },
            new ApiError{ Code = Codes.FieldNotValid, Message = "El valor del campo no es valido. " },
            new ApiError{ Code = Codes.FieldNull, Message = "El campo es Obligatorio. Campo en cuestion: " },
            new ApiError{ Code = Codes.FieldDuplicate, Message = "Error de Duplicacíon: " },
            new ApiError{ Code = Codes.FieldMissing, Message = "Error de Valor requerido: " },
            new ApiError{ Code = Codes.DontMatch, Message = "No coinciden"},
            new ApiError{ Code = Codes.UncreatedResource, Message = "El recurso no se creo. recurso en cuestion: " },
            new ApiError{ Code = Codes.LoguinError, Message = "No pudo acceder: " },
            new ApiError{ Code = Codes.CriticanlError, Message = "Error Critico: " },
            new ApiError{ Code = Codes.NotFound, Message = "Recurso no encontrado: " },
            new ApiError{ Code = Codes.UserNotConfirm, Message = "Email de usuario no confirmado: " }
        };
    }

}