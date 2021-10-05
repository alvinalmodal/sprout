using Microsoft.Extensions.Configuration;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Sprout.Exam.DataAccess.Implementations
{
    public class EmployeeRepository : IDBRepository<EmployeeModel>
    {
        public string ConnectionName { get; set; } = "DefaultConnection";
        private IConfiguration Config;
        private string Connection;

        public EmployeeRepository(IConfiguration config)
        {
            Config = config;
            Connection = config.GetConnectionString(ConnectionName);
        }

        public List<EmployeeModel> All()
        {
            var employees = new List<EmployeeModel>();

            using (var connection = new SqlConnection(Connection))
            {
                employees = connection.Query<EmployeeModel>(
                    "usp_Employee_List", 
                    new { }, 
                    commandType : System.Data.CommandType.StoredProcedure
                ).ToList();
            }

            return employees;
        }

        public void Remove(EmployeeModel employee)
        {
            using (var connection = new SqlConnection(Connection))
            {
                connection.Execute(
                    "usp_Employee_Remove", 
                    new { Id = employee.Id }, 
                    commandType: System.Data.CommandType.StoredProcedure
                );
            }
        }

        public void Save(EmployeeModel employee)
        {
            using (var connection = new SqlConnection(Connection))
            {
                connection.Execute("usp_Employee_Save", employee, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public List<EmployeeModel> Search(EmployeeModel employee)
        {
            var employees = new List<EmployeeModel>();

            using (var connection = new SqlConnection(Connection))
            {
               employees = connection.Query<EmployeeModel>(
                   "usp_Employee_Search", 
                   new { FullName = employee.FullName}, 
                   commandType: System.Data.CommandType.StoredProcedure
               ).ToList();
            }

            return employees;
        }

        public void Update(EmployeeModel employee)
        {
            using (var connection = new SqlConnection(Connection))
            {
                connection.Execute("usp_Employee_Update", employee, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}