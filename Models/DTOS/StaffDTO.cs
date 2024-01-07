using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProjekt.Models.DTOS
{
    internal class StaffDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string? Role { get; set; }
        public int ?DaysEmployeed { get; set; }

    }
}
