using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeProject;
using System.Collections.Generic;

namespace EmployeeProjectUnitTests
{
    [TestClass]
    public class EmployeeProjectTests
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    // Arrange
        //    // Act
        //    // Assert
        //}

        //public void MethodName_Scenario_ExpectedBehaviour
        //public void CanBeCalcelledBy_IsAdmin_ReturnsTrue


        // Initial Data 
        List<Employee> allEmployees = new List<Employee>
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
        EmployeeController controller = new EmployeeController();


        [TestMethod]
        public void AddEmployee_AddingNewValidEmployee_AddNewValue()
        {            
            // Arrange
            Employee employee = new Employee()
            {
                EmployeeId = 100,
                Forename = "John",
                Surname = "Snow", 
                Email = "js@gmail.com",
                Position = 0
            };

            // Arrange 
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddEmployee_AddingNewInvalidEmployee_ShouldNotAdd()
        {
            // Arrange
            Employee employee = new Employee()
            {
                EmployeeId = 101,
                Forename = "Bill",
                Surname = "Nye",
                Email = "bn",
                Position = 0
            };


            // Arrange 
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            // Assert
            Assert.IsFalse(result);
        }



        [TestMethod]
        public void DeleteEmployee_DeletingNewEmployee_ShouldNotMatch()
        {
            // Arrange
            List<Employee> toUpdateEmployees = new List<Employee>
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
            var employeeId = 1;

            // Arrange 
            List<Employee> employees = controller.DeleteEmployeeMK2(employeeId, toUpdateEmployees).Result;

            // Assert
            CollectionAssert.AreNotEqual(allEmployees, employees);
        }



    }
}