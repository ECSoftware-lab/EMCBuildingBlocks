using System.Reflection;

namespace EMC.BuildingBlocks.Application.Pagination
{
    public class IsPropertyValidnousar<T>
    {
        //validar si esta bien eeste metodo 
        public T? evaluar { get; set; }
        public static bool IsValid(string propName)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.Name == propName)
                {
                    return true; // La propiedad existe en la clase T
                }
            }

            return false; // La propiedad no existe en la clase T
        }
    }

}
