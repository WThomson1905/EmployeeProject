// See https://aka.ms/new-console-template for more information



using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace EmployeeProject
{
    public class Program
    {


        private static async Task Main(string[] args)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee() {
                    EmployeeId = 1,
                    Forename = "Tom",
                    Surname = "Cruise",
                    Email = "tc@g.com",
                    Position = EmployeeType.Manager
                },
                 new Employee() {
                    EmployeeId = 2,
                    Forename = "Brad",
                    Surname = "Pitt",
                    Email = "bp@g.com",
                    Position = EmployeeType.Engineer
                },
                  new Employee() {
                    EmployeeId = 3,
                    Forename = "Bill",
                    Surname = "Sandler",
                    Email = "bs@g.com",
                    Position = EmployeeType.Intern
                },
                new Employee() {
                    EmployeeId = 4,
                    Forename = "Jack",
                    Surname = "Sheppard",
                    Email = "js@g.com",
                    Position = EmployeeType.Engineer
                },
                 new Employee() {
                    EmployeeId = 5,
                    Forename = "John",
                    Surname = "Locke",
                    Email = "jl@g.com",
                    Position = EmployeeType.Engineer
                },
                  new Employee() {
                    EmployeeId = 6,
                    Forename = "Kate",
                    Surname = "Auston",
                    Email = "ka@g.com",
                    Position = EmployeeType.Engineer
                },
            };
            // 
            // SerialiseObjectToJsonString();
            await SerializeToFile(employees);
            //DeserizalizeEmployeeJson();

            Console.WriteLine("Employee Project!");

            
            StartApp();

        }

        private static async void StartApp()
        {
            var allEmployees = DeserizalizeEmployeeJson();
            //var jsonSerializer = JsonSerializer.Serialize(allEmployees);
            //var listOfEmployees = JsonSerializer.Deserialize<List<Employee>>(jsonSerializer);


            Console.WriteLine("Choose Your Option: 1 - Display All Employees 2 - Add Employee, 3 - Delete Employee 4 - Update Position, 5 - Filter Employees \n");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    DisplayAllEmployees(allEmployees);
                    StartApp();
                    break;
                case "2":
                    Console.WriteLine("Opt2: Add Employee");
                    StartApp();
                    break;
                case "3":
                    Console.WriteLine("Opt3: Remove Employee");
                    StartApp();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Opt4: Choose Employee To Update\n");
                    DisplayAllEmployees(allEmployees);

                    Console.WriteLine("Choose Employee Id\n");
                    var chooseId = Console.ReadLine();
                    await UpdateEmployeeAsync(Convert.ToInt32(chooseId));
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
            Console.WriteLine("Choose position type: 1 - Manager 2 - Engineer, 3 - Intern \n");
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
            // get all employees from JSON and store in List<Employee>
            // filter based on parameter
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
            var fileName = @"C:\Users\William\source\repos\EmployeeProject\EmployeeProject\Employees.json";
            Console.WriteLine(File.ReadAllText(fileName));
            using var stream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(stream, employees);
            await stream.DisposeAsync();
        }


        private static List<Employee> DeserizalizeEmployeeJson()
        {
            var filePath = new StreamReader(@"C:\Users\William\source\repos\EmployeeProject\EmployeeProject\Employees.json");
            var employees = JsonSerializer.Deserialize<List<Employee>>(filePath.ReadToEnd());

            return employees;
        }



        //public static List<Employee> GetAllEmployees()
        //{
        //    List<Employee> employees = DeserizalizeEmployeeJson();
        //    return employees;
        //}

        private static void DisplayAllEmployees(List<Employee> listOfEmployees)
        {
           
            if (listOfEmployees != null)
            {
                foreach (var employee in listOfEmployees)
                {
                    Console.WriteLine("Employee Id: " + employee.EmployeeId);
                    Console.WriteLine("Full Name: " + employee.Forename + " " + employee.Surname);
                    Console.WriteLine("Position: " + employee.Position);
                    Console.WriteLine();
                }
            } else
            {
                Console.WriteLine("No Employees!!");
            }
            //Console.ReadLine();
            //Console.Clear();
             
        }



        private static void AddEmployee()
        {
            // Create a new JsonObject using object initializers.
            var employeeObject = new JsonObject
            {
                ["employeeId"] = 9,
                ["forename"] = "Tommy",
                ["surname"] = "Gun",
                ["email"] = "tg@g.com",
                ["position"] = 0

            };
         
            //// Add an object.
            //forecastObject["TemperatureRanges"]["Hot"] =
            //    new JsonObject { ["High"] = 60, ["Low"] = 20 };

            //// Remove a property.
            //forecastObject.Remove("SummaryWords");

            //// Change the value of a property.
            //forecastObject["Date"] = new DateTime(2019, 8, 3);

            //var options = new JsonSerializerOptions { WriteIndented = true };
            //Console.WriteLine(forecastObject.ToJsonString(options));
        }
        
        
        private static async Task UpdateEmployeeAsync(int employeeId)
        {

            List<Employee> employees = DeserizalizeEmployeeJson();
            int currentId;

            foreach (var employee in employees)
            {
                currentId = employee.EmployeeId;

                if (currentId == employeeId)
                {
                    employee.Position = EmployeeType.Intern;
                }
            }
            var fileName = new StreamReader(@"C:\Users\William\source\repos\EmployeeProject\EmployeeProject\Employees.json");


            using var stream = File.Create(@"C:\Users\William\source\repos\EmployeeProject\EmployeeProject\Employees.json");
            await JsonSerializer.SerializeAsync(stream, employees);
            await stream.DisposeAsync();
            //await SerializeToFile(employees); 
            DisplayAllEmployees(employees);

        }


        private static void UpdateEmployee2(int employeeId)
        {
            // find employee by id
            // change option to engineer by default for now!
            // 

            var filePath = new StreamReader(@"C:\Users\William\source\repos\EmployeeProject\EmployeeProject\Employees.json");
            var jsonString = filePath.ReadToEnd();
            
           
            int position2 = 0;
       

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


        }
        

    }

    //
    // Display all (filtered) employees
    // 





}






//string jsonString = File.ReadAllText(@"C:\Users\William\source\repos\EmployeeProject\EmployeeProject\Employees.json");
//Console.WriteLine(jsonString);

//var employees = JsonSerializer.Deserialize<ObservableCollection<Employee>>(jsonString);