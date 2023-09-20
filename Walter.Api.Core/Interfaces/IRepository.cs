using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Walter.Api.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : class, IEntity
{
	Task Save();
	Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
	Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);
	Task<IEnumerable<TEntity>> GetAll();
	Task<TEntity?> GetById(object id);
	Task Insert(TEntity entity);
	Task Update(TEntity entityToUpdate);
	Task Delete(TEntity entity);
	Task Delete(object id);
}
