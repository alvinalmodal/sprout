using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Interfaces;
using AutoMapper;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService EmployeeServices;
        private readonly IMapper Mapper;

        public EmployeesController(IEmployeeService employeeServices, IMapper mapper)
        {
            EmployeeServices = employeeServices;
            Mapper = mapper;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await EmployeeServices.All();
            var result = Mapper.Map<List<EmployeeDto>>(employees);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await EmployeeServices.SearchById(id);
            if (employee == null) return NotFound();
            var result = Mapper.Map<EditEmployeeDto>(employee);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto employeeInput)
        {
            var employee = await EmployeeServices.SearchById(employeeInput.Id);
            if (employee == null) return NotFound();
            var employeeForUpdating = Mapper.Map<EmployeeModel>(employeeInput);
            await EmployeeServices.Update(employeeForUpdating);
            return Ok(employeeInput);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var newEmployee = Mapper.Map<EmployeeModel>(input);
            var id = await EmployeeServices.Create(newEmployee);

            return Created($"/api/employees/{id}", id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeForDeletion = await EmployeeServices.SearchById(id);
            if (employeeForDeletion == null) return NotFound();
            await EmployeeServices.Remove(employeeForDeletion);
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(CalculateSalaryDto input)
        {
            var result = await EmployeeServices.SearchById(input.Id);

            if (result == null) return NotFound();
            var netIncome = await EmployeeServices.CalculateSalary(
                new CalculateSalaryDto {
                    Id = result.Id,
                    AbsentDays = input.AbsentDays,
                    WorkedDays = input.WorkedDays
                }
            );

            return Ok(netIncome);
        }

    }
}
