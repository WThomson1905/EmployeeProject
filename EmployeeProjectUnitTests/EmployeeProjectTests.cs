using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeProject;
using System.Collections.Generic;
using System.Linq;
using System;

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
        //public void CanBeCancelledBy_IsAdmin_ReturnsTrue


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

            // Act 
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddEmployee_EmailNotValid_ShouldNotAdd()
        {
            // Arrange - invalid email
            Employee employee = new Employee()
            {
                EmployeeId = 101,
                Forename = "Bill",
                Surname = "Nye",
                Email = "bn",
                Position = 0
            };

            // Act 
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddEmployee_ForenameNotValid_ShouldNotAdd()
        {
            //Arrange - Invalid Forename
            Employee employee = new Employee()
            {
                EmployeeId = 1000,
                Forename = "",
                Surname = "Nye",
                Email = "bn@gmail.com",
                Position = 0
            };

            //Act
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddEmployee_SurnameNotValid_ShouldNotAdd()
        {
            //Arrange - Invalid Forename
            Employee employee = new Employee()
            {
                EmployeeId = 1000,
                Forename = "Bill",
                Surname = "",
                Email = "bn@gmail.com",
                Position = 0
            };

            //Act
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddEmployee_IdNotValid_ShouldNotAdd()
        {
            // Arrange - invalid id
            Employee employee = new Employee()
            {
                EmployeeId = -1,
                Forename = "Bill",
                Surname = "Nye",
                Email = "bn@gmail.com",
                Position = 0
            };


            // Act 
            bool result = controller.AddEmployee(employee, allEmployees).Result;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddEmployee_PositionNotValid_ShouldNotAdd()
        {
            // Arrange - invalid email
            Employee employee = new Employee()
            {
                EmployeeId = 789,
                Forename = "Bill",
                Surname = "Nye",
                Email = "bn@gmail.com",
                Position = (EmployeeType)5
            };


            // Act 
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
            var employeeId = 2;

            // Act 
            List<Employee> employees = controller.DeleteEmployee(employeeId, toUpdateEmployees).Result;

            // Assert
            CollectionAssert.AreNotEqual(allEmployees, employees);
        }


        [TestMethod]
        public void DeleteEmployee_DeletingNewEmployee_ShouldNotMatchMK2()
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

            // Act 
            List<Employee> employees = controller.DeleteEmployee(employeeId, toUpdateEmployees).Result;
            bool areEqual = allEmployees.Equals(employees);

            // Assert
            Assert.IsFalse(areEqual);
        }




        [TestMethod]
        public void DeleteEmployee_DeletingNonExistentEmployee_ShouldNotFind()
        {
            // Arrange
            string path = "C:\\Users\\Aley\\source\\repos\\EmployeeProject\\EmployeeProject\\Employees.json";
            List<Employee> allEmployees = controller.DeserizalizeEmployeeJson(path);
            var employeeId = 100000;
            bool couldFindId = true;

            // Act 
            if (allEmployees.Any(e => e.EmployeeId == employeeId))
            {
                List<Employee> deletedEmployee = controller.DeleteEmployee(employeeId, allEmployees).Result;
                couldFindId = true;
            }
            else
            {
                couldFindId = false;
            }

            // Assert
            Assert.IsFalse(couldFindId);
        }


        [TestMethod]
        public void DeserizalizeEmployeeJson_DeserizalizeEmployeeJson_ShouldNotFind()
        {
            // Arrange
            string path = @"C:\Users\Aley\source\repos\EmployeeProject\EmployeeProject\TestJsonFiles\EmployeesInvalid.json";


            // Act 
            List<Employee> deserialized = controller.DeserizalizeEmployeeJson(path);

      
            // Assert
            Assert.IsNotNull(deserialized);
        }

    }
}