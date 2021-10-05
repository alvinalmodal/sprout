using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public interface IDBRepository<T> where T : class
    {
        void Save(T value);
        List<T> All();
        void Update(T value);
        void Remove(T value);
        List<T> Search(T value);

    }
}
