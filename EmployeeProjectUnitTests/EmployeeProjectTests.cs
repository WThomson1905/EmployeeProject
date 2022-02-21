using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeProject;
using System.Collections.Generic;

namespace EmployeeProjectUnitTests
{
    [TestClass]
    public class EmployeeProjectTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            // Act
            //Assert
        }

        //public void MethodName_Scenario_ExpectedBehaviour
        //public void CanBeCalcelledBy_IsAdmin_ReturnsTrue


        // write tests for different sceanrios



        [TestMethod]
        public void AddEmployee_AddingNewEmployeeValid_WillAdd()
        {
            Employee employee = new Employee()
            {
                EmployeeId = 100,
                Forename = "John",
                Surname = "Snow", 
                Email = "js@gmail",
                Position = 0
            };

            Program.AddEmployee(employee);
        }

        [TestMethod]
        public void DeleteEmployee_DeletingNewEmployeeValid_WillDelete()
        {
            // Arrange 
            var employeeId = 122;
               
            Program.DeleteEmployeeMK2(employeeId);
        }


    }
}