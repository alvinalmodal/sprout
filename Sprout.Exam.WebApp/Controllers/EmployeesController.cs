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
using FluentValidation;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService EmployeeServices;
        private readonly IMapper Mapper;
        private readonly IValidator<CreateEmployeeDto> CreateValidator;
        private readonly IValidator<EditEmployeeDto> UpdateValidator;
        private readonly IValidator<CalculateSalaryDto> CalculateValidator;

        public EmployeesController(
            IEmployeeService employeeServices, 
            IMapper mapper, 
            IValidator<CreateEmployeeDto> createValidator,
            IValidator<EditEmployeeDto> updateValidator,
            IValidator<CalculateSalaryDto> calculateValidator
         )
        {
            EmployeeServices = employeeServices;
            Mapper = mapper;
            CreateValidator = createValidator;
            UpdateValidator = updateValidator;
            CalculateValidator = calculateValidator;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await EmployeeServices.All();
                var result = Mapper.Map<List<EmployeeDto>>(employees);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new List<ErrorResponse> { new ErrorResponse { Key = this.HttpContext.Request.Method, ErrorMessage = ex.Message } } });
            }
            
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await EmployeeServices.SearchById(id);
                if (employee == null) return NotFound();
                var result = Mapper.Map<EditEmployeeDto>(employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new List<ErrorResponse> { new ErrorResponse { Key = this.HttpContext.Request.Method, ErrorMessage = ex.Message } } });
            }
            
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto employeeInput)
        {
            try
            {
                var modelState = UpdateValidator.Validate(employeeInput);
                if (!modelState.IsValid)
                    return BadRequest(new { Errors = Mapper.Map<List<ErrorResponse>>(modelState.Errors) });

                var employee = await EmployeeServices.SearchById(employeeInput.Id);
                if (employee == null) return NotFound();
                var employeeForUpdating = Mapper.Map<EmployeeModel>(employeeInput);
                await EmployeeServices.Update(employeeForUpdating);
                return Ok(employeeInput);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new List<ErrorResponse> { new ErrorResponse { Key = this.HttpContext.Request.Method, ErrorMessage = ex.Message } } });
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            try
            {
                var modelState = CreateValidator.Validate(input);
                if (!modelState.IsValid)
                    return BadRequest(new { Errors = Mapper.Map<List<ErrorResponse>>(modelState.Errors) });

                var newEmployee = Mapper.Map<EmployeeModel>(input);
                var id = await EmployeeServices.Create(newEmployee);

                return Created($"/api/employees/{id}", id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new List<ErrorResponse> { new ErrorResponse { Key = this.HttpContext.Request.Method , ErrorMessage = ex.Message } } });
            }

        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employeeForDeletion = await EmployeeServices.SearchById(id);
                if (employeeForDeletion == null) return NotFound();
                await EmployeeServices.Remove(employeeForDeletion);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new List<ErrorResponse> { new ErrorResponse { Key = this.HttpContext.Request.Method, ErrorMessage = ex.Message } } });
            }
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
            try
            {
                var modelState = CalculateValidator.Validate(input);
                if (!modelState.IsValid)
                    return BadRequest(new { Errors = Mapper.Map<List<ErrorResponse>>(modelState.Errors) });

                var result = await EmployeeServices.SearchById(input.Id);

                if (result == null) return NotFound();
                var netIncome = await EmployeeServices.CalculateSalary(
                    new CalculateSalaryDto
                    {
                        Id = result.Id,
                        AbsentDays = input.AbsentDays,
                        WorkedDays = input.WorkedDays
                    }
                );

                return Ok(netIncome);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new List<ErrorResponse> { new ErrorResponse { Key = this.HttpContext.Request.Method, ErrorMessage = ex.Message } } });
            }

        }

    }
}
