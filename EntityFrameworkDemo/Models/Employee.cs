using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDemo.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public decimal Salary
        {
            get;
            set;
        }
        public string Designation
        {
            get;
            set;
        }
    }
}
