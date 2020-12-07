using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class EFSqlProducts : ILookupProducts
    {
        public Task<GetProductDetailResponse> GetProductById(int id)
        {
            return Task.FromResult( new GetProductDetailResponse
            {
                Id = id,
                Name = "Some Product",
                Category = "Bread",
                Count = 1,
                Price = 1.89M
            });
        }
    }
}
