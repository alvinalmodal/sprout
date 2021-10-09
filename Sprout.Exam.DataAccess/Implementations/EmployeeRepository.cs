using Microsoft.Extensions.Configuration;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Implementations
{
    public sealed class EmployeeRepository : IDBRepository<EmployeeModel>, IDisposable
    {
        private IConfiguration Config;
        private readonly SqlConnection Connection;

        public EmployeeRepository(IConfiguration config)
        {
            Config = config;
            Connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }

        public async Task<List<EmployeeModel>> All()
        {
            var employees = await Connection.QueryAsync<EmployeeModel>(
                "usp_Employee_List",
                new { },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return employees.ToList();
        }

        public async Task<EmployeeModel> Remove(EmployeeModel employee)
        {
            return await Connection.QueryFirstOrDefaultAsync<EmployeeModel>(
                "usp_Employee_Remove",
                new { Id = employee.Id },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<EmployeeModel> Save(EmployeeModel employee)
        {
            return await Connection.QueryFirstOrDefaultAsync<EmployeeModel>(
                "usp_Employee_Save",
                new { 
                        FullName = employee.FullName, 
                        BirthDate = employee.BirthDate, 
                        TIN = employee.TIN, 
                        EmployeeTypeId = employee.EmployeeTypeId 
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<List<EmployeeModel>> Search(EmployeeModel employee)
        {

            var employees = await Connection.QueryAsync<EmployeeModel>(
                   "usp_Employee_Search",
                   new { FullName = employee.FullName },
                   commandType: System.Data.CommandType.StoredProcedure
            );

            return employees.ToList();
        }

        public async Task<EmployeeModel> SearchById(int id)
        {
            return await Connection.QueryFirstOrDefaultAsync<EmployeeModel>(
                   "usp_Employee_SearchById",
                   new { Id = id },
                   commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<EmployeeModel> Update(EmployeeModel employee)
        {
            return await Connection.QueryFirstOrDefaultAsync<EmployeeModel>(
                "usp_Employee_Update", 
                employee, 
                commandType: 
                System.Data.CommandType.StoredProcedure
            );
        }

        public void Dispose()
        {
            if(Connection != null)
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
                Connection.Dispose();
            }
        }
    }
}