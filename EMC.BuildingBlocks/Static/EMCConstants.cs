using System.Collections;
using System.Collections.Generic;

namespace EMC.BuildingBlocks.Static
{
    public class EMCConstants
    {
        public class TypeFilterProd
        {
            public const int ContainedText = 1;
            public const int ExactText = 2;
            public const int StartText = 3;
            public const int NotSerchText = 4;
        }

        public static Dictionary<int, string> TypeFilterProdList = new Dictionary<int, string>
            {
        { TypeFilterProd.ContainedText, "Busco contenido en, " },
        { TypeFilterProd.ExactText, "Busco coincidencia exacta " },
        { TypeFilterProd.StartText, "Busco al principio" } ,
        { TypeFilterProd.NotSerchText, "" }
         };




        public static List<string> ExtensionsImagen = new List<string> { "jpg", "jpeg", "png", "gif", "bmp" };

        public class DocumentType
        {
            public const int DNI = 1;
            public const int CUIL = 2;
            public const int CUIT = 3;
            public const int CDI = 4;
            public const int Pp = 5;
            public const int CI_Ext = 6;
            public const int CUITPais = 7;
            public const int Otro = 8;
        };

        public class LevelLog
        {
            public const int Critical = 6;
            public const int Error = 5;
            public const int Warning = 4;
            public const int notImpl = 3;
            public const int notImple = 2;
            public const int Info = 1;
        }
        //public static List<SelectDTO> DocumentTypeList = new List<SelectDTO>
        //{
        //    new SelectDTO { Id = DocumentType.DNI, Name = "DNI" },
        //    new SelectDTO { Id = DocumentType.CUIL, Name = "CUIL" },
        //    new SelectDTO { Id = DocumentType.CUIT, Name = "CUIT" },
        //    new SelectDTO { Id = DocumentType.CDI, Name = "CDI" },
        //    new SelectDTO { Id = DocumentType.Pp, Name = "PASAPORTE" },
        //    new SelectDTO { Id = DocumentType.CI_Ext, Name = "CI_Ext" },
        //    new SelectDTO { Id = DocumentType.CUITPais, Name = "CUIT PAIS" },
        //    new SelectDTO { Id = DocumentType.Otro, Name = "OTRO" },
        //};


        public class PersonTypeConst
        {
            public const int Empleado = 1;
            public const int Cliente = 2;
            public const int Proveedor = 3;
            public const int Contador = 4;
            public const int Visitante = 5;
        }

        public static Dictionary<int, string> PersonTypeList = new Dictionary<int, string>
            {
        { PersonTypeConst.Cliente, "Cliente" },
        { PersonTypeConst.Empleado, "Empleado" },
        { PersonTypeConst.Proveedor, "Proveedor" },
        { PersonTypeConst.Contador, "Contador" },
        { PersonTypeConst.Visitante, "Visitante" }
         };

        // public static List<ProductAtributeTypeDTO> ProductAtributeTypeList = new List<ProductAtributeTypeDTO>


        public class Attributes
        {

            public const string Ages = "EDAD";
            public const string Gender = "GENERO";
            public const string Orders = "ORDEN";
            public const string DeliverySlip = "DELIVERYSLIP"; /*remitos*/
            public const string Season = "TEMPORADA";
            public const string Measure = "UNIDAD";
            public const string MeasureType = "TIPO UNIDAD";
        }

        public static List<string> AttributesList = new()
        {
        Attributes.Ages,
        Attributes.Gender,
        Attributes.Season,
        Attributes.Orders,
        Attributes.DeliverySlip,
        Attributes.Measure,
        Attributes.MeasureType,
        };

        public class soThat
        {
            public const string Register = "ADD";
            public const string Update = "UPDATE";
            public const string Gets = "GET";
        }


        public static List<string> soThatList = new()
        {
        soThat.Register,
        soThat.Register,
        soThat.Register
        };

        public class LevelUser
        {

            public const int Simple = 0;
            public const int WithRoles = 1;

            public const int WithPerson = 2;

            public const int WithType = 3;
            public const int WithAddress = 4;
            public const int WithCities = 5;
        }
        public class LevelPerson
        {

            public const int Full = 1;
            public const int WithType = 2;
            public const int WithEmail = 3;
            public const int WithPhone = 4;
            public const int WithAddress = 5;
            public const int WithAudit = 6;
            public const int WithUser = 7;
            public const int WithCities = 8;
            public const int OnlyType = 9;
            public const int WithDetails = 10;
            public const int WithDetailsUser = 11;

        }
        public class MilestoneType
        {

            public const int Creada = 1;
            public const int Devuelta = 2;
            public const int Anulada = 3;
            public const int Cancelada = 4;
            public const int Finalizada = 5;
            public const int Procesando = 6;
            public const int Procesada = 7;
        }
        public class MilestonePurcharse : MilestoneType
        {
            //mayores que 6 
            public const int EnDistibucion = 10;
            public const int Distribuida = 11;
        }

        //public static List<MilestoneDto> MilestonePurcharseList = new List<MilestoneDto>
        // {
        //new MilestoneDto {Id = MilestonePurcharse.Creada, Name = "Creada"},
        //new MilestoneDto {Id = MilestonePurcharse.Devuelta, Name = "Devuelta"},
        //new MilestoneDto {Id = MilestonePurcharse.Anulada, Name = "Anulada"},
        //new MilestoneDto {Id = MilestonePurcharse.Cancelada, Name = "Cancelada"},
        //new MilestoneDto {Id = MilestonePurcharse.Finalizada, Name = "Finalizada"},
        //new MilestoneDto {Id = MilestonePurcharse.Procesando, Name = "Procesando"},
        //new MilestoneDto {Id = MilestonePurcharse.Procesada, Name = "Procesada"},
        //new MilestoneDto {Id = MilestonePurcharse.EnDistibucion, Name = "EnDistibucion"},
        //new MilestoneDto {Id = MilestonePurcharse.Distribuida, Name = "Distribuida"}
        //};

        public class DateFormats
        {
            public const string DateAndHour = "dd/MM/yyyy HH:mm:ss";
            public const string OnlyDate = "dd/MM/yyyy";
        }
    }
}
