using System.Reflection;

namespace EMC.BuildingBlocks.Application.Pagination
{
    public class IsPropertyValid<T>
    {
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
