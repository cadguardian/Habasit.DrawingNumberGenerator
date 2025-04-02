using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SB_Plastics.DataAccess
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<int> InsertAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(int id);
    }
}