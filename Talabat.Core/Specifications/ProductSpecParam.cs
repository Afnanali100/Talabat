using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParam
    {
        private const int maxsize= 10;
        private int pagesize = 5;
        public int PageSize {
            get
            { return pagesize; }
            
            set 
            { pagesize = value > 10 ? 10 : value; } 
                           }

        public int PageIndex { get; set; } = 1;

        public string? sort {  get; set; }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }


        private string name { get; set; }
        public string? Search {
            get
            {return name; }
            set { name = value.ToLower(); } }   


    }
}
