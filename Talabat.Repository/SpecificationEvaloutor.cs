using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaloutor<TEntity> where TEntity : BaseEntity
    {
        

        public static  IQueryable<TEntity> GenerateQuery(IQueryable<TEntity> InputQuery , ISpecification<TEntity> spec)
        {
            var query=InputQuery;
            if(spec.Ceriteia != null)
                query=query.Where(spec.Ceriteia);
            if(spec.OrderBy != null)
                query=query.OrderBy(spec.OrderBy);
            if(spec.OrderByDescending != null)
                query=query.OrderByDescending(spec.OrderByDescending);

            if(spec.IsPaginated)
                query=query.Skip(spec.Skip).Take(spec.Take);



            query=spec.Includes.Aggregate(query,(str1,str2)=>str1.Include(str2));

            return query;
        }



    }
}
