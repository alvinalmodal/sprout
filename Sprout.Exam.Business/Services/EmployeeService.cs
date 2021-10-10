using Microsoft.Extensions.Configuration;
using Sprout.Exam.DataAccess.Implementations;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Sprout.Exam.DataAccess.Models;
using System.Threading.Tasks;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Factories;
using Sprout.Exam.Business.Models;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : IEmployeeService, IDisposable
    {
        public EmployeeRepository EmployeeDb { get; set; }
        public EmployeeService(IConfiguration config)
        {
            EmployeeDb = new EmployeeRepository(config);
        }

        public async Task<List<EmployeeModel>> All()
        {
            return await EmployeeDb.All();
        }

        public async Task<EmployeeModel> Create(EmployeeModel employee)
        {
            return await EmployeeDb.Save(employee);
        }

        public async Task<EmployeeModel> Update(EmployeeModel employee)
        {
            return await EmployeeDb.Update(employee);
        }

        public async Task<EmployeeModel> Remove(EmployeeModel employee)
        {
            return await EmployeeDb.Remove(employee);
        }

        public async Task<EmployeeModel> SearchById(int id)
        {
            return await EmployeeDb.SearchById(id);
        }

        public async Task<double> CalculateSalary(CalculateSalaryDto payrollDetails)
        {
            var employee = await EmployeeDb.SearchById(payrollDetails.Id);

            var computationDetails = new ComputationDetailsModel {
                TypeId = employee.EmployeeTypeId,
                RatePerDay = 500,
                Absences = payrollDetails.AbsentDays,
                DaysReported = payrollDetails.WorkedDays,
                TaxRate = 0.12,
                MonthlySalary = 20000
            };

            var calculatorFactory = new SalaryCalculatorFactory();
            var calculator = calculatorFactory.GetCalculator(computationDetails);
            var netIncome = calculator.ComputeSalary();
            return netIncome;
        }

        public void Dispose()
        {
            if (EmployeeDb != null)
                EmployeeDb.Dispose();
        }

    }
}
