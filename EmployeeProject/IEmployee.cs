using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject
{
    public interface IEmployee
    {
        int EmployeeId { get; set; }

        string Forename { get; set; }

        string Surname { get; set; }
        string Email { get; set; }

        EmployeeType Position { get; set; }
    }
}
