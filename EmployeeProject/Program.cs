using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace EmployeeProject
{
    public class Program
    {
        public static string path = "C:\\Users\\William\\source\\repos\\EmployeeProject\\EmployeeProject\\Employees.json";
        private static async Task Main(string[] args)
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
                    }
                };
                await SerializeToFile(employees);
            }
            else
            {
                employees = DeserizalizeEmployeeJson();
            }
         
            Console.WriteLine("Employee Project!");
            StartApp();
        }

        private static async void StartApp()
        {
            var allEmployees = DeserizalizeEmployeeJson();
            
            Console.WriteLine("Choose Your Option: 1 - Display All Employees, 2 - Add Employee, 3 - Delete Employee, 4 - Update Position, 5 - Filter Employees \n");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    DisplayAllEmployees(allEmployees);
                    StartApp();
                    break;
                case "2":
                    Employee employee = new Employee(); 


                    Console.WriteLine("Opt2: Add Employee");
                    Console.WriteLine("Choose Employee Id: \n");
                    employee.EmployeeId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Choose Employee Forename: \n");
                    employee.Forename = Console.ReadLine();

                    Console.WriteLine("Choose Employee Surname: \n");
                    employee.Surname = Console.ReadLine();

                    Console.WriteLine("Choose Employee Email: \n");
                    employee.Email = Console.ReadLine();

                    Console.WriteLine("Choose Employee Position: 0 - manager, 1 - engineer, 2 - intern\n");
                    var employeePosition = Console.ReadLine();
                    employee.Position = (EmployeeType)Convert.ToInt32(employeePosition);


                    AddEmployee(employee);
                    StartApp();
                    break;
                case "3":
                    Console.WriteLine("Opt3: Remove Employee");
                    var chosenId = Console.ReadLine();
                    DeleteEmployeeMK2(Convert.ToInt32(chosenId));
                    StartApp();
                    break;

                case "4": // Update Employee Type: Manager, Engineer, Intern
                    Console.Clear();
                    Console.WriteLine("Opt4: Choose Employee To Update\n");
                    DisplayAllEmployees(allEmployees);

                    Console.WriteLine("Choose Employee Id: \n");
                    var chooseId = Console.ReadLine();

                    Console.WriteLine("Choose Position: 0 - Manager, 1 - Engineer, 2 - Intern\n");
                    var choosePosition = Convert.ToInt32(Console.ReadLine());

                    UpdateEmployeeAsync(Convert.ToInt32(chooseId), (EmployeeType)choosePosition);
                    StartApp();
                    break;
                case "5":
                    Console.WriteLine("Opt5: Filter Employees");
                    ChooseFilterEmployeesOption();
                    StartApp();
                    break;
                default:
                    StartApp();
                    break;
            }

            Console.ReadKey();
        }

        private static void ChooseFilterEmployeesOption()
        {
            Console.WriteLine("Choose position type: 1 - Manager, 2 - Engineer, 3 - Intern \n");
            var option = Console.ReadLine();
            var employees = new List<Employee>(); 
            switch (option)
            {
                case "1":
                    employees = FilterEmployees(EmployeeType.Manager);
                    DisplayAllEmployees(employees);
                    break;
                case "2":
                    employees = FilterEmployees(EmployeeType.Engineer);
                    DisplayAllEmployees(employees);
                    break;
                case "3":
                    employees = FilterEmployees(EmployeeType.Intern);
                    DisplayAllEmployees(employees);
                    break;
                default:
                    Console.Clear();
                    ChooseFilterEmployeesOption();
                    break;
            }
        }

        private static List<Employee> FilterEmployees(EmployeeType position)
        {
            List<Employee> employees = DeserizalizeEmployeeJson();
            List<Employee> employeesFileteredByPosition = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if(employee.Position == position)
                {
                    employeesFileteredByPosition.Add(employee);
                }
            }

            return employeesFileteredByPosition;
        }

        //public static void SerialiseObjectToJsonString()
        //{
        //    List<Employee> employees = new List<Employee>
        //    {
        //        new Employee() {
        //            EmployeeId = 1,
        //            Forename = "Tom",
        //            Surname = "Cruise",
        //            Position = EmployeeType.Manager
        //        },
        //         new Employee() {
        //            EmployeeId = 2,
        //            Forename = "Brad",
        //            Surname = "Pitt",
        //            Position = EmployeeType.Engineer
        //        },
        //          new Employee() {
        //            EmployeeId = 3,
        //            Forename = "Bill",
        //            Surname = "Sandler",
        //            Position = EmployeeType.Intern
        //        },
        //        new Employee() {
        //            EmployeeId = 4,
        //            Forename = "Jack",
        //            Surname = "Sheppard",
        //            Position = EmployeeType.Engineer
        //        },
        //         new Employee() {
        //            EmployeeId = 5,
        //            Forename = "John",
        //            Surname = "Locke",
        //            Position = EmployeeType.Engineer
        //        },
        //          new Employee() {
        //            EmployeeId = 6,
        //            Forename = "Kate",
        //            Surname = "Auston",
        //            Position = EmployeeType.Engineer
        //        },
        //    };
        //    var jsonSerializer = JsonSerializer.Serialize(employees);
        //    Console.WriteLine(jsonSerializer);
        //    Console.WriteLine("Ahhhhhhhhhhhhh");
        //}

        private static async Task SerializeToFile(List<Employee> employees)
        { 
            //Console.WriteLine(File.ReadAllText(path));
            using (var stream = File.Create(path))
            {
                await JsonSerializer.SerializeAsync(stream, employees);
                await stream.DisposeAsync();
                stream.Close();
            }

        }


        private static List<Employee> DeserizalizeEmployeeJson()
        {
            var filePath = new StreamReader(path);
            var employees = JsonSerializer.Deserialize<List<Employee>>(filePath.ReadToEnd());
            filePath.Close();
            return employees;
        }
        private static void DisplayAllEmployees(List<Employee> listOfEmployees)
        {
            if (listOfEmployees != null)
            {
                foreach (var employee in listOfEmployees)
                {
                    Console.WriteLine("Employee Id: " + employee.EmployeeId);
                    Console.WriteLine("Full Name: " + employee.Forename + " " + employee.Surname);
                    Console.WriteLine("Emial: " + employee.Email);
                    Console.WriteLine("Position: " + employee.Position);
                    Console.WriteLine();
                }
            } 
            else
            {
                Console.WriteLine("No Employees!!");
            }
            //Console.ReadLine();
            //Console.Clear();
        }

        private static async Task AddEmployee(Employee employee)
        {
            //Write json data
            List<Employee> employees = DeserizalizeEmployeeJson();

            employees.Add(employee);
            await SerializeToFile(employees);

            Console.WriteLine(employee.Forename);
        }


        private static async Task UpdateEmployeeAsync(int employeeId, EmployeeType selectedEmployeeType)
        {

            List<Employee> employees = DeserizalizeEmployeeJson();
            int currentId;

            foreach (var employee in employees)
            {
                currentId = employee.EmployeeId;

                if (currentId == employeeId)
                {
                    ChangeEmployeePosition(employee.Position, selectedEmployeeType);

                   

                    employee.Position = selectedEmployeeType;
                }
            }

            await SerializeToFile(employees);
          
        }

        private static void ChangeEmployeePosition(EmployeeType currentEmployeeType, EmployeeType selectedEmployeeType)
        {
            // business logic for checking whether you can change emp position
            // manager != intern
            // engineer != intern

            if (currentEmployeeType == EmployeeType.Manager && selectedEmployeeType == EmployeeType.Intern)
            {
                Console.WriteLine("Can't change manager to intern!");
                StartApp();
            }
            if (currentEmployeeType == EmployeeType.Engineer && selectedEmployeeType == EmployeeType.Intern)
            {
                Console.WriteLine("Can't change Engineer to intern!");
                StartApp();
            }

        }



        private static void UpdateEmployee(int employeeId)
        {
            // find employee by id
            // change option to engineer by default for now!
            // 

            var filePath = new StreamReader(path);
            var jsonString = filePath.ReadToEnd();


            //int position2 = 0;


            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {

                JsonElement root = document.RootElement;
                // JsonElement studentsElement = root.GetProperty("Students");
                foreach (JsonElement employee in root.EnumerateArray())
                {
                    int currentId = employee.GetProperty("employeeId").GetInt32();

                    if (currentId == employeeId)
                    {

                        JsonNode? positionNode = JsonNode.Parse(employee.ToString());
                        // Write JSON from a JsonNode
                        Console.WriteLine(employee.ToString());
                        //employee["forename"] = "billybob";

                        //employee["forename"] = "1";

                        //position2 = employeeIdElement.GetInt32();
                        //Console.WriteLine($"EmployeeId : {position2}");
                    }
                }
            }

            //    ////////////////
            //    // source JSON to process
            //    Console.WriteLine(jsonString);

            //    // root node (opening curly brace)
            //    List<string>? keys = JsonNode.Parse(jsonString)?.AsArray();

            //    List<string> keys = jsonString.AsObject().Select(
            //        child => child.Key).ToList();
            //    if (root != null)
            //    {
            //        List<Employee> currentId = root.Deserialize();

            //        for (int i = 0; i <= root.Count(); i++)
            //        {

            //            if (currentId == employeeId)
            //            {

            //                JsonNode? positionNode = JsonNode.Parse(employee.ToString());
            //                Console.WriteLine(employee.ToString());
            //            }
            //        }
            //    }




            //    // if the root contains no key named "container" 
            //    //JsonNode? containerNode = root?["container"];

            //    //if (containerNode == null)
            //    //{
            //    //    return;
            //    //}

            //    // get the names of the keys under the container key
            //    //List<string> keys = root.AsArray();

            //    // convert read-only JsonNode to writable JsonObject 
            //    //JsonObject container = containerNode.AsObject();

            //    // iterate and move keys from container to root
            //    foreach (string key in keys)
            //    {
            //        //JsonNode? move = containerNode[key];
            //        //container.Remove(key);
            //        //root?.Add(key, move);
            //    }

            //    root?.Remove("container");

            //    Console.WriteLine(root);

        }

        //deletes employee from the db
        private static async Task DeleteEmployeeMK2(int employeeId)
        {
            List<Employee> employees = DeserizalizeEmployeeJson();
            int currentId;

            int itemCount = employees.Count;

            Console.WriteLine("count of items before deleting:{0}", employees.Count);

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
                            //mylist.RemoveAt(i);
                        }
                    }
                }
            }

            await SerializeToFile(employees);
            Console.WriteLine("count of items after deleting:{0}", employees.Count);
        }   
    }
}

