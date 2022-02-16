﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeProject
{
    public class Employee : IEmployee
    {
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
    }

    public enum EmployeeType
    {
        Manager, Engineer, Intern
    }
}