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


        public async Task<List<Employee>> DefineEmployees()
        {
            List<Employee> employees;
            if (new FileInfo(path).Length == 0)
            {
                employees = new List<Employee>
                {
                    new Employee()
                    {
                        EmployeeId = 1,
                        Forename = "Tom",
                        Surname = "Cruise",
                        Email = "tc@g.com",
                        Position = EmployeeType.Manager
                    },
                    new Employee()
                    {
                        EmployeeId = 2,
                        Forename = "Brad",
                        Surname = "Pitt",
                        Email = "bp@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Employee()
                    {
                        EmployeeId = 3,
                        Forename = "Bill",
                        Surname = "Sandler",
                        Email = "bs@g.com",
                        Position = EmployeeType.Intern
                    },
                    new Employee()
                    {
                        EmployeeId = 4,
                        Forename = "Jack",
                        Surname = "Sheppard",
                        Email = "js@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Employee()
                    {
                        EmployeeId = 5,
                        Forename = "John",
                        Surname = "Locke",
                        Email = "jl@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Employee()
                    {
                        EmployeeId = 6,
                        Forename = "Kate",
                        Surname = "Auston",
                        Email = "ka@g.com",
                        Position = EmployeeType.Engineer
                    },
                    new Employee()
                    {
                        EmployeeId = 7,
                        Forename = "Ahhhhhhhh",
                        Surname = "Smith",
                        Email = "js@g.com",
                        Position = EmployeeType.Intern
                    },
                    new Employee()
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




        public List<Employee> FilterEmployees(EmployeeType position, List<Employee> employees)
        {
            //List<Employee> employees = DeserizalizeEmployeeJson(path);
            List<Employee> employeesFileteredByPosition = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if (employee.Position == position)
                {
                    employeesFileteredByPosition.Add(employee);
                }
            }

            return employeesFileteredByPosition;
        }


        public async Task<bool> AddEmployee(Employee employee, List<Employee> employees)
        {
            //Write json data
            employees.Add(employee);
            bool validJsonToSchema = await CheckJsonWithSchema(employees, schemaPath);
            if (validJsonToSchema)
            {
                await SerializeToFile(employees, path);
                return true;
            }
            return false;
        }


        public async Task<bool> UpdateEmployeeAsync(int employeeId, EmployeeType selectedEmployeeType)
        {
            List<Employee> employees = DeserizalizeEmployeeJson(path);
            int currentId;
            bool validJsonToSchema = false; 
            foreach (var employee in employees)
            {
                currentId = employee.EmployeeId;

                if (currentId == employeeId)
                {
                    employee.Position = selectedEmployeeType;
                }
            }
            validJsonToSchema = CheckJsonWithSchema(employees, schemaPath).Result;

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
            List<Employee> employees = DeserizalizeEmployeeJson(path);
            Employee currentEmployee = employees.First(e => e.EmployeeId == currentEmployeeId);

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

        //public int ChangeEmployeePosition(EmployeeType currentEmployeeType, EmployeeType selectedEmployeeType)
        //{
        //    // business logic for checking whether you can change emp position
        //    // manager be changed to intern
        //    // engineer be changed to intern

        //    if (currentEmployeeType == EmployeeType.Manager && selectedEmployeeType == EmployeeType.Intern)
        //    {
        //        return 1;
        //        ///Display.StartApp();
        //    }
        //    if (currentEmployeeType == EmployeeType.Engineer && selectedEmployeeType == EmployeeType.Intern)
        //    {
        //        return 2;
        //        ///Display.StartApp();
        //    }
        //    return 0;
        //}



        //public void UpdateEmployee(int employeeId)
        //{
        //    // find employee by id
        //    // change option to engineer by default for now! 

        //    var filePath = new StreamReader(path);
        //    var jsonString = filePath.ReadToEnd();

        //    using (JsonDocument document = JsonDocument.Parse(jsonString))
        //    {

        //        JsonElement root = document.RootElement;

        //        foreach (JsonElement employee in root.EnumerateArray())
        //        {
        //            int currentId = employee.GetProperty("employeeId").GetInt32();

        //            if (currentId == employeeId)
        //            {
        //                JsonNode? positionNode = JsonNode.Parse(employee.ToString());
        //                //Console.WriteLine(employee.ToString());
        //            }
        //        }
        //    }
        //}

        //deletes employee from the db


        public async Task<List<Employee>> DeleteEmployee(int employeeId, List<Employee> employees)
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




        private async Task<bool> CheckJsonWithSchema(List<Employee> employees, string jsonSchemaPath)
        {
            var jsonSchema = await JsonSchema.FromFileAsync(jsonSchemaPath);
            var jsonString = JsonSerializer.Serialize(employees);
            var errors = jsonSchema.Validate(jsonString);

            if(errors.Count != 0)
                return false;

            return true;
        }

        public async Task SerializeToFile(List<Employee> employees, string jsonPath)
        {
            using (var stream = File.Create(jsonPath))
            {
                await JsonSerializer.SerializeAsync(stream, employees);
                await stream.DisposeAsync();
                stream.Close();
            }
        }


        public List<Employee> DeserizalizeEmployeeJson(string path)
        {
            try
            {
                var filePath = new StreamReader(path);
                var employees = JsonSerializer.Deserialize<List<Employee>>(filePath.ReadToEnd());
                filePath.Close();
                return employees;
            } catch (JsonException ex)
            {
                throw new JsonException();  
            }
        }
    }

}
