//Application to allow a user to retrieve a list of employees, Add to the list, delete from the list
//edit an employee's position to allow promotion and demotion, and to filter through the list of
//employees by position.

using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace EmployeeProject
{
    public class Program
    {
        //The Path to where the JSON file is stored to be used throughout the program
        public static string path = "C:\\Users\\Aley\\source\\repos\\EmployeeProject\\EmployeeProject\\Employees.json";
        
        private static async Task Main(string[] args)
        {
            //Gets the list of employees and checks if the JSON file has values
            List<Employee> employees;
            if (new FileInfo(path).Length == 0)
            {
                //if JSON file has no values, it is populated with these values
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
                //if JSON file has values, the contents are read
                employees = DeserizalizeEmployeeJson();
            }
         
            Console.WriteLine("Employee Project!");
            StartApp();
        }

        //Console Applications start screen displaying options
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

        //Allows user to choose the position they wish to filter through the employees by position
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

        //Filters the employees by their position
        private static List<Employee> FilterEmployees(EmployeeType position)
        {
            List<Employee> employees = DeserizalizeEmployeeJson();
            List<Employee> employeesFileteredByPosition = new List<Employee>();

            
            foreach (Employee employee in employees)
            {
                //if employee position matches the position entered by the user
                if (employee.Position == position)
                {
                    //Add the employee to the filtered list
                    employeesFileteredByPosition.Add(employee);
                }
            }

            return employeesFileteredByPosition;
        }

        //Serializes the JSON file when it is updated/created
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

        //Deserializes the JSON file
        private static List<Employee> DeserizalizeEmployeeJson()
        {
            var filePath = new StreamReader(path);
            var employees = JsonSerializer.Deserialize<List<Employee>>(filePath.ReadToEnd());
            filePath.Close();
            return employees;
        }

        //Displays all the employees stored inside the JSON file
        private static void DisplayAllEmployees(List<Employee> listOfEmployees)
        {
            //if the lost of employees has values
            if (listOfEmployees != null)
            {
                //display their details
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
        }

        //Allows user to add an employee
        private static async Task AddEmployee(Employee employee)
        {
            //Write json data
            List<Employee> employees = DeserizalizeEmployeeJson();
            employees.Add(employee);
            await SerializeToFile(employees);

            Console.WriteLine(employee.Forename);
        }

        //Updates the employee selected by the user
        private static async Task UpdateEmployeeAsync(int employeeId, EmployeeType selectedEmployeeType)
        {

            List<Employee> employees = DeserizalizeEmployeeJson();
            int currentId;

            foreach (var employee in employees)
            {
                currentId = employee.EmployeeId;

                //if the id chosen is the same as employee id in the list
                if (currentId == employeeId)
                {
                    //calls ChangeEmployeePosition and changes the current position to the chosen position
                    ChangeEmployeePosition(employee.Position, selectedEmployeeType);

                    employee.Position = selectedEmployeeType;
                }
            }

            await SerializeToFile(employees);
          
        }

        //call to ensure that the correct position has been selected else throws error message
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


        //Update method that isnt used within the application
        private static void UpdateEmployee(int employeeId)
        {
            // find employee by id
            // change option to engineer by default for now! 

            var filePath = new StreamReader(path);
            var jsonString = filePath.ReadToEnd();

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
                        Console.WriteLine(employee.ToString());
                    }
                }
            }   
        }

        //deletes employee from the list
        private static async Task DeleteEmployeeMK2(int employeeId)
        {
            List<Employee> employees = DeserizalizeEmployeeJson();
            int currentId;

            //counts the number of employees in the list before deleting
            int itemCount = employees.Count;

            Console.WriteLine("count of items before deleting:{0}", employees.Count);

            foreach (var employee in employees.ToList())
            {
                currentId = employee.EmployeeId;

                if (currentId == employeeId)
                {

                    for (int i = itemCount - 1; i >= 0; i--)
                    {
                        //if the id chosen matches the current id of the employee 
                        if (employees[i].EmployeeId == currentId)
                        {
                            //employee is deleted
                            employees.Remove(employees[i]);
                        }
                    }
                }
            }

            //saves to file and then counts the number of employees in the list after deleting
            //used to check if delete actually works
            await SerializeToFile(employees);
            Console.WriteLine("count of items after deleting:{0}", employees.Count);
        }   
    }
}

