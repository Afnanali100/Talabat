using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IBasketRepository
    {

        public Task<CustomerBasket> GetBasketAsync (string basketId);

        public Task<bool> DeleteBasketAsync(string basketId);

        public Task<CustomerBasket> UpdateBacketAsync(CustomerBasket customerBasket);


    }
}
