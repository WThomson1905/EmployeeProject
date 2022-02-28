using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EmployeeProject
{
    public class EmployeeController
    {

        public static string path = "C:\\Users\\William\\source\\repos\\EmployeeProject\\EmployeeProject\\Employees.json";
        public static string schemaPath = "C:\\Users\\William\\source\\repos\\EmployeeProject\\EmployeeProject\\schema.json";


        public async Task<List<EmployeeBase>> DefineEmployees()
        {
            List<EmployeeBase> employees;
            if (new FileInfo(path).Length == 0)
            {
                employees = new List<EmployeeBase>
                {
                    new Manager()
                    {
                        EmployeeId = 1,
                        Forename = "Tom",
                        Surname = "Cruise",
                        Email = "tc@g.com",
                        Position = EmployeeType.Manager
                    },
                    new Engineer()
                    {
                        EmployeeId = 2,
                        Forename = "Brad",
                        Surname = "Pitt",
                        Email = "bp@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Intern()
                    {
                        EmployeeId = 3,
                        Forename = "Bill",
                        Surname = "Sandler",
                        Email = "bs@g.com",
                        Position = EmployeeType.Intern
                    },
                    new Engineer()
                    {
                        EmployeeId = 4,
                        Forename = "Jack",
                        Surname = "Sheppard",
                        Email = "js@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Engineer()
                    {
                        EmployeeId = 5,
                        Forename = "John",
                        Surname = "Locke",
                        Email = "jl@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Engineer()
                    {
                        EmployeeId = 6,
                        Forename = "Kate",
                        Surname = "Auston",
                        Email = "ka@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Intern()
                    {
                        EmployeeId = 7,
                        Forename = "Ahhhhhhhh",
                        Surname = "Smith",
                        Email = "js@g.com",
                        Position = EmployeeType.Intern
                    },
                    new Manager()
                    {
                        EmployeeId = 8,
                        Forename = "Tommy",
                        Surname = "Cruisey",
                        Email = "tc@g.com",
                        Position = EmployeeType.Manager
                    }
                };
                await SerializeToFile(employees, path);
            }
            else
            {
                employees = DeserizalizeEmployeeJson(path);
            }

            return employees;
        }




        public List<EmployeeBase> FilterEmployees(EmployeeType position, List<EmployeeBase> employees)
        {
            //List<Employee> employees = DeserizalizeEmployeeJson(path);
            List<EmployeeBase> employeesFileteredByPosition = new List<EmployeeBase>();

            foreach (EmployeeBase employee in employees)
            {
                if (employee.Position == position)
                {
                    employeesFileteredByPosition.Add(employee);
                }
            }

            return employeesFileteredByPosition;
        }


        public async Task<bool> AddEmployee(EmployeeBase employee, List<EmployeeBase> employees)
        {
            //Write json data
            bool validJsonToSchema = await CheckJsonWithSchema(employee, schemaPath);
            employees.Add(employee);
            if (validJsonToSchema)
            {
                await SerializeToFile(employees, path);
                return true;
            }
            return false;
        }


        public async Task<bool> UpdateEmployeeAsync(int employeeId, EmployeeType selectedEmployeeType)
        {
            List<EmployeeBase> employees = DeserizalizeEmployeeJson(path);
            int currentId;
            bool validJsonToSchema = false; 
            foreach (var employee in employees)
            {
                currentId = employee.EmployeeId;

                if (currentId == employeeId)
                {
                    employee.Position = selectedEmployeeType;
                    validJsonToSchema = CheckJsonWithSchema(employee, schemaPath).Result;
                }
            }

            if (validJsonToSchema)
            {
                await SerializeToFile(employees, path);
                return true;
            }
            return false;
        }

        public bool ValidChangeEmployeePositionRequest(int currentEmployeeId, EmployeeType selectedEmployeeType)
        {
            // business logic for checking whether you can change emp position
            // manager be changed to intern
            // engineer be changed to intern
            List<EmployeeBase> employees = DeserizalizeEmployeeJson(path);
            EmployeeBase currentEmployee = employees.First(e => e.EmployeeId == currentEmployeeId);

            if (currentEmployee.Position == EmployeeType.Manager && selectedEmployeeType == EmployeeType.Intern)
            {
                return false;
            }
            else if (currentEmployee.Position == EmployeeType.Engineer && selectedEmployeeType == EmployeeType.Intern)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

       


        public async Task<List<EmployeeBase>> DeleteEmployee(int employeeId, List<EmployeeBase> employees)
        {

            int currentId;

            int itemCount = employees.Count;

            foreach (var employee in employees.ToList())
            {
                currentId = employee.EmployeeId;

                if (currentId == employeeId)
                {

                    for (int i = itemCount - 1; i >= 0; i--)
                    {
                        if (employees[i].EmployeeId == currentId)
                        {
                            employees.Remove(employees[i]);
                        }
                    }
                }
            }

            await SerializeToFile(employees, path);

            return employees;
        }




        private async Task<bool> CheckJsonWithSchema(EmployeeBase employee, string jsonSchemaPath)
        {
            List<EmployeeBase> employees = new List<EmployeeBase>
                { employee };
                
            var jsonSchema = await JsonSchema.FromFileAsync(jsonSchemaPath);
            var jsonString = JsonSerializer.Serialize(employees);
            var errors = jsonSchema.Validate(jsonString);

            if(errors.Count != 0)
                return false;

            return true;
        }

        public async Task SerializeToFile(List<EmployeeBase> employees, string jsonPath)
        {
            using (var stream = File.Create(jsonPath))
            {
                await JsonSerializer.SerializeAsync(stream, employees);
                await stream.DisposeAsync();
                stream.Close();
            }
        }


        public List<EmployeeBase> DeserizalizeEmployeeJson(string path)
        {
            try
            {
                var filePath = new StreamReader(path);
                var employees = JsonSerializer.Deserialize<List<EmployeeBase>>(filePath.ReadToEnd());
                filePath.Close();
                return employees;
            } catch (JsonException ex)
            {
                throw new JsonException();  
            }
        }
    }

}


