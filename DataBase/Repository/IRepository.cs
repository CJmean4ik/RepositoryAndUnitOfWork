using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{

    // CRUD
    internal interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllEntitiesAsync();
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
