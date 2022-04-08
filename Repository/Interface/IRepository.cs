using Api_MaestroDetalle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetLatest();
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}
