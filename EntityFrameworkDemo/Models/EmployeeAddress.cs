using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.Models
{
    public class EmployeeAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }
    }
}
