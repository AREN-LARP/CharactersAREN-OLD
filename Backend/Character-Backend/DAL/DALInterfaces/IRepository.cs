using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> PutEntity(TEntity entity);
        Task<TEntity> AddEntity(TEntity entity);
        Task<TEntity> DeleteEntity(TEntity entity);
    }
}
