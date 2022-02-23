using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject
{
    public class Display
    {

        public static string path = "C:\\Users\\Aley\\source\\repos\\EmployeeProject\\EmployeeProject\\Employees.json";
        public static string schemaPath = "C:\\Users\\Aley\\source\\repos\\EmployeeProject\\EmployeeProject\\schema.json";
        
        
        private EmployeeController _employeeController = new EmployeeController();
        private List<Employee> employees;
        private List<Employee> employeesFilter;
        private Employee employee;



        public Display()
        {

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
                _employeeController.SerializeToFile(employees, path);
            }
            else
            {
                employees = _employeeController.DeserizalizeEmployeeJson(path);
            }

            StartApp();
        }

        public void StartApp()
        {
            employees = _employeeController.DeserizalizeEmployeeJson(path);

            Console.WriteLine("Choose Your Option: 1 - Display All Employees, 2 - Add Employee, 3 - Delete Employee, 4 - Update Position, 5 - Filter Employees \n");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayAllEmployees(employees);
                    StartApp();
                    break;

                case "2":
                    employee = new Employee();


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

                    bool value = _employeeController.AddEmployee(employee, employees).Result;
                    Console.Clear();
                    if (value)
                        Console.WriteLine("Added Successfully!"); 
                    else
                        Console.WriteLine("Unsuccessfully Added!");


                    StartApp();
                    break;



                case "3": // Remove Employee 
                    Console.WriteLine("Opt3: Remove Employee - pick EmployeeId");
                    var chosenId = Console.ReadLine();
                    Console.WriteLine("count of items before deleting:{0}", employees.Count);
                    if (employees.Any(e => e.EmployeeId == Convert.ToInt32(chosenId)))
                    {
                        employees = _employeeController.DeleteEmployeeMK2(Convert.ToInt32(chosenId), employees).Result;
                        Console.WriteLine("count of items after deleting:{0}", employees.Count);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find Id chosen! ");
                    }

               
                    StartApp();
                    break;

                case "4": // Update Employee Type: Manager, Engineer, Intern
                    Console.Clear();
                    Console.WriteLine("Opt4: Choose Employee To Update\n");
                    DisplayAllEmployees(employees);

                    Console.WriteLine("Choose Employee Id: \n");
                    var chooseId = Console.ReadLine();

                    Console.WriteLine("Choose Position: 0 - Manager, 1 - Engineer, 2 - Intern\n");
                    var choosePosition = Convert.ToInt32(Console.ReadLine());


                    // validateInput
                    bool valid = ValidChangeEmployeePositionRequest(Convert.ToInt32(chooseId), (EmployeeType)choosePosition);
                    // if (valid) update employee
                    if (valid)
                    {
                        _employeeController.UpdateEmployeeAsync(Convert.ToInt32(chooseId), (EmployeeType)choosePosition);

                    }
                    StartApp();
                    break;
                case "5":
                    Console.WriteLine("Opt5: Filter Employees");
                    employeesFilter = ChooseFilterEmployeesOption();
                    DisplayAllEmployees(employeesFilter);
                    StartApp();
                    break;
                default:
                    StartApp();
                    break;

                
            }
            Console.ReadKey();
        }



        public void DisplayAllEmployees(List<Employee> listOfEmployees)
        {
            if (listOfEmployees != null)
            {
                foreach (var employee in listOfEmployees)
                {
                    Console.WriteLine("Employee Id: " + employee.EmployeeId);
                    Console.WriteLine("Full Name: " + employee.Forename + " " + employee.Surname);
                    Console.WriteLine("Email: " + employee.Email);
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


        public bool ValidChangeEmployeePositionRequest(int currentEmployeeId, EmployeeType selectedEmployeeType)
        {
            // business logic for checking whether you can change emp position
            // manager be changed to intern
            // engineer be changed to intern

            Employee currentEmployee = employees.First(e => e.EmployeeId == currentEmployeeId);

            if (currentEmployee.Position == EmployeeType.Manager && selectedEmployeeType == EmployeeType.Intern)
            {
                Console.WriteLine("Can't change manager to intern!");
                return false;
            }
            if (currentEmployee.Position == EmployeeType.Engineer && selectedEmployeeType == EmployeeType.Intern)
            {
                Console.WriteLine("Can't change Engineer to intern!");
                return false;
            }
            return true;
        }



        public List<Employee> ChooseFilterEmployeesOption()
        {
            Console.WriteLine("Choose position type: 1 - Manager, 2 - Engineer, 3 - Intern \n");
            var option = Console.ReadLine();
            var employees = new List<Employee>();
            switch (option)
            {
                case "1":
                    employees = _employeeController.FilterEmployees(EmployeeType.Manager);
                    break;
                case "2":
                    employees = _employeeController.FilterEmployees(EmployeeType.Engineer);
                    break;
                case "3":
                    employees = _employeeController.FilterEmployees(EmployeeType.Intern);
                    break;
                default:
                    ChooseFilterEmployeesOption();
                    break;
            }
            return employees;
        }


    }
}
