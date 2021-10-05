using Microsoft.Extensions.Configuration;
using Sprout.Exam.DataAccess.Implementations;
using Sprout.Exam.DataAccess.Models;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Sprout.Exam.Tests
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository Db;

        public EmployeeRepositoryTest()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            Db = new EmployeeRepository(config);
        }

        [Fact]
        public void Search_ShouldReturnListOfEmployesBasedOnFullName()
        {
            var employee = Db.Search(new EmployeeModel { FullName = "Alvin" }).FirstOrDefault();
            Assert.Equal("Alvin Almodal", employee.FullName);
        }
    }
}
