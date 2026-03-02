
namespace Shared.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        public uint MinAge { get; set; } = 18;
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxAge > MinAge;
        public string? SearchTerm { get; set; }

    }
}
