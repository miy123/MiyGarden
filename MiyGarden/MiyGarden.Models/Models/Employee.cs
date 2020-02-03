namespace MiyGarden.Models.Models
{
    internal class Employee
    {
        public int GetYearsEmployed() => 5;

        public virtual string GetProgressReport() => "Employee's GetProgressReport";

        public static Employee Lookup(string name) => new Manager { Name = name };
    }
}
