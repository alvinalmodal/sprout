using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public interface IDBRepository<T> : IDisposable
    {
        Task<EmployeeModel> Save(T value);
        Task<List<T>> All();
        Task<EmployeeModel> Update(T value);
        Task<EmployeeModel> Remove(T value);
        Task<List<T>> Search(T value);
        Task<EmployeeModel> SearchById(int id);
    }
}
