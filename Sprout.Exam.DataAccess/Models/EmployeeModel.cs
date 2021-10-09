using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.DataAccess.Models
{
    public class EmployeeModel
    {
        [Write(false)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public EmployeeType EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(Object obj)
        {
            var employeeToCompare = (EmployeeModel)obj;

            if (
                this.Id == employeeToCompare.Id
                && this.FullName == employeeToCompare.FullName
                && this.BirthDate.ToString("yyyy-MM-dd") == employeeToCompare.BirthDate.ToString("yyyy-MM-dd")
                && this.TIN == employeeToCompare.TIN
                && this.EmployeeTypeId == employeeToCompare.EmployeeTypeId
                && this.IsDeleted == employeeToCompare.IsDeleted
            )
                return true;

            return false;
        }
    }
}
