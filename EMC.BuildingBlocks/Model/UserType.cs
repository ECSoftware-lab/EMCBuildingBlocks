namespace EMC.BuildingBlocks.Model;

public enum UserType
{
    ROOT,
    AdminT2,
    AdminT1,
    EmpleadoT2,
    EmpleadoT1,
    ClienteT2,
    CLienteT1,
    Basico,
    Visitante,
}
public static class UserTypeList
{
    public static readonly List<(string Name, int Value)> Types = Enum.GetValues(typeof(UserType))
        .Cast<UserType>()
        .Select(x => (x.ToString(), (int)x))
        .ToList();

    public static readonly List<(string Name, int Value)> TypesAdmin = Enum.GetValues(typeof(UserType))
      .Cast<UserType>().Where(x => x == UserType.AdminT2 || x == UserType.AdminT1 || x == UserType.ROOT)
      .Select(x => (x.ToString(), (int)x))
      .ToList();

    public static readonly List<(string Name, int Value)> TypesEmploye = Enum.GetValues(typeof(UserType))
     .Cast<UserType>().Where(x => x == UserType.EmpleadoT1 || x == UserType.EmpleadoT2)
     .Select(x => (x.ToString(), (int)x))
     .ToList();

    public static readonly List<(string Name, int Value)> TypesCustomer = Enum.GetValues(typeof(UserType))
   .Cast<UserType>().Where(x => x == UserType.ClienteT2 || x == UserType.CLienteT1 || x == UserType.Visitante)
   .Select(x => (x.ToString(), (int)x))
   .ToList();

    public static readonly HashSet<string> AdminRoleNames = TypesAdmin.Select(x => x.Name).ToHashSet();
    public static readonly HashSet<string> EmployeRoleNames = TypesEmploye.Select(x => x.Name).ToHashSet();
    public static readonly HashSet<string> CustomerRoleNames = TypesCustomer.Select(x => x.Name).ToHashSet();

    public static List<string> GetTypesBelowValue(int b)
    {
        return Enum.GetValues(typeof(UserType))
            .Cast<UserType>()
            .Where(x => (int)x > b)
            .Select(x => x.ToString())
            .ToList();
    }
    public static Dictionary<int, string> GetRolBelowValue(int b)
    {
        return Enum.GetValues(typeof(UserType))
            .Cast<UserType>()
            .Where(x => (int)x > b)
            .ToDictionary(x => (int)x, x => x.ToString());
    }


    public static List<(string Name, int Value)> MapRolesToUserTypes(List<string> roles)
    {
        var mappedTypes = new List<(string Name, int Value)>();

        foreach (var role in roles)
        {
            if (Enum.TryParse<UserType>(role, out var userType))
            {
                var userTypeValue = (int)userType;
                mappedTypes.Add((role, userTypeValue));
            }
        }

        return mappedTypes;
    }

    public static Dictionary<int, string> GetRolBelowValue(string b)
    {
        if (!Enum.TryParse(b, out UserType userType))
        {
            throw new ArgumentException("Invalid UserType value.", nameof(b));
        }

        return Enum.GetValues(typeof(UserType))
            .Cast<UserType>()
            .Where(x => (int)x > (int)userType)
            .ToDictionary(x => (int)x, x => x.ToString());
    }

    public static int? GetNumericValue(string userTypeString)
    {
        if (Enum.TryParse(userTypeString, out UserType userType))
        {
            return (int)userType;
        }
        else
        {
            return null;
        }

    }
    public static string GetUserTypeString(int numericValue)
    {
        if (Enum.IsDefined(typeof(UserType), numericValue))
        {
            return Enum.GetName(typeof(UserType), numericValue);
        }
        else
        {
            throw new ArgumentException("Invalid numeric value for UserType.", nameof(numericValue));
        }
    }
}