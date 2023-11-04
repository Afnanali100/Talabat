using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>) await storeContext.Product.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
            }
            return await storeContext.Set<T>().ToListAsync();
        }

     

        public async Task<T> GetByIdAsync(int id)
        {
           //if(typeof(T)== typeof(Product))
              //  return  T await  storeContext.Product.Where(p=>p.Id==id).Include(p=>p.ProductBrand).Include(p=>p.ProductType).FirstOrDefaultAsync();

            return await storeContext.Set<T>().FindAsync(id);
        }


        public async Task<IReadOnlyList<T>> GetAllBySpecificationAsync(ISpecification<T> spec)
        {
            return await ApplySpecificationEvaluator(spec).ToListAsync();
        }
        public async Task<T> GetByIdSpecificationAsync(ISpecification<T> spec)
        {
            return await ApplySpecificationEvaluator(spec).FirstOrDefaultAsync();
        }


        public async Task <int>GetProductCountSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecificationEvaluator(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecificationEvaluator(ISpecification<T> spec)
        {
            var query =  SpecificationEvaloutor<T>.GenerateQuery(storeContext.Set<T>(), spec);
            return query;
        }

       
    }
}
