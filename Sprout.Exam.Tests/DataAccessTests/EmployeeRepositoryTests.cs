using Microsoft.Extensions.Configuration;
using Sprout.Exam.DataAccess.Implementations;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Tests.Helpers;
using System;
using System.IO;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace Sprout.Exam.Tests
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository Db;
        private TestDBHelper DbHelper;

        public EmployeeRepositoryTest()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            Db = new EmployeeRepository(config);
            DbHelper = new TestDBHelper(config);
        }

        public static IEnumerable<object[]> GetSaveTestData =>
            new List<object[]>
            {
                    new object[] {
                        new EmployeeModel {
                            FullName = "Pedro Dela Cruz",
                            BirthDate = DateTime.Now,
                            EmployeeTypeId = EmployeeType.Contractual,
                            IsDeleted = false,
                            TIN = "99998"
                        }
                    },
                    new object[] {
                        new EmployeeModel {
                            FullName = "Maria Dela Cruz",
                            BirthDate = DateTime.Now,
                            EmployeeTypeId = EmployeeType.Regular,
                            IsDeleted = false,
                            TIN = "99997"
                        }
                    },
            };

        private EmployeeModel CreateTestEmployee()
        {
            DbHelper.TruncateTable("Employee");
            var employee = new EmployeeModel
            {
                FullName = "Juan Dela Cruz",
                BirthDate = DateTime.Now,
                EmployeeTypeId = EmployeeType.Regular,
                IsDeleted = false,
                TIN = "99999"
            };
            return Db.Save(employee).Result;
        }

        [Fact]
        public void Search_ShouldReturnListOfEmployesBasedOnFullName()
        {
            var employee = CreateTestEmployee();
            var employeeInDB = Db.Search(new EmployeeModel { FullName = employee.FullName }).Result.FirstOrDefault();
            Assert.Equal(employee, employeeInDB);
        }

        [Theory]
        [MemberData(nameof(GetSaveTestData))]
        public void Save_ShouldAddNewEmployee(EmployeeModel employee)
        {
            DbHelper.TruncateTable("Employee");
            var employeeInDB = Db.Save(employee).Result;
            Assert.Equal(employee.FullName, employeeInDB.FullName);
        }

        [Fact]
        public void Update_ShouldUpdateEmployeeInformation()
        {
            var employee = CreateTestEmployee();
            var employeeInDb = Db.Search(employee).Result.FirstOrDefault();

            employeeInDb.FullName = "Updated Juan Dela Cruz";
            employeeInDb.BirthDate = DateTime.Now;
            employeeInDb.EmployeeTypeId = EmployeeType.Contractual;
            employeeInDb.TIN = "8888";
            
            var updatedEmployee = Db.Update(employeeInDb).Result;

            Assert.Equal(employeeInDb, updatedEmployee);
        }

        [Theory]
        [MemberData(nameof(GetSaveTestData))]
        public void Remove_ShouldRemoveEmployeeInDB(EmployeeModel employee)
        {
            DbHelper.TruncateTable("Employee");
            var newEmployee = Db.Save(employee).Result;

            var removedEmployee = Db.Remove(newEmployee).Result;

            Assert.Equal(newEmployee.Id, removedEmployee.Id);
        }

        [Theory]
        [MemberData(nameof(GetSaveTestData))]
        public void SearchById_ShouldReturnEmployeeFromDB(EmployeeModel employee)
        {
            DbHelper.TruncateTable("Employee");
            var newEmployee = Db.Save(employee).Result;

            var searchedEmployee = Db.SearchById(newEmployee.Id).Result;

            Assert.Equal(newEmployee, searchedEmployee);
        }

    }
}
