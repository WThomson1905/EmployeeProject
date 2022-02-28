using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeProject
{

    abstract class EmployeeBase 
    {

        public EmployeeBase()
        {
            EmployeeId = 0;
            Forename = "";
            Surname = "";
            Email = "";
            Position = new EmployeeType();
        }

        public EmployeeBase(int employeeId, string forename, string surname, string email, EmployeeType position)
        {
            EmployeeId = employeeId;
            Forename = forename;
            Surname = surname;
            Email = email;
            Position = position;
        }


        [JsonPropertyName("employeeId")]
        [JsonInclude]
        public int EmployeeId { get; set; }

        [JsonPropertyName("forename")]
        [JsonInclude]
        public string Forename { get; set; }

        [JsonPropertyName("surname")]
        [JsonInclude]
        public string Surname { get; set; }

        [JsonPropertyName("email")]
        [JsonInclude]
        public string Email { get; set; }

        [JsonPropertyName("position")]
        [JsonInclude]
        public EmployeeType Position { get; set; }



        public abstract void DoWork(); 

    }
}
