using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using NJsonSchema;

namespace EmployeeProject
{
    public class Program
    {

        static void Main(string[] args)
        {
            EmployeeBase employee = new Manager()
            {
                EmployeeId = 10001,
                Forename = "Billy",
                Surname = "Bob",
                Email = "bb@b.bom",
                Position = 0
            }; 

            employee.DoWork();


            Display display = new Display();

        }
    }
}

