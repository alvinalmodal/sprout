using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> All();
        Task<EmployeeModel> Create(EmployeeModel employee);
        Task<EmployeeModel> Update(EmployeeModel employee);
        Task<EmployeeModel> Remove(EmployeeModel employee);

        Task<EmployeeModel> SearchById(int id);

        Task<double> CalculateSalary(CalculateSalaryDto payrollDetails);

    }
}
