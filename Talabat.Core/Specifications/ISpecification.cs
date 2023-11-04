using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        //(p=>p.id==id)
        public Expression<Func<T, bool>> Ceriteia { get; set; }

        //(p>p.id)
        public List<Expression<Func<T,object>>> Includes {set; get;}

        public Expression<Func<T,object>> OrderBy { set; get;}

        public Expression<Func<T, object>> OrderByDescending { set; get; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPaginated { get; set; }


    }
}
