using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        public   Task<IReadOnlyList<T>> GetAllAsync() ;


        public Task<T> GetByIdAsync(int id);


        public Task<IReadOnlyList<T>> GetAllBySpecificationAsync(ISpecification<T> spec);


        public Task<T> GetByIdSpecificationAsync(ISpecification<T> spec);


        public Task<int> GetProductCountSpecAsync (ISpecification<T> spec);
    }
}
