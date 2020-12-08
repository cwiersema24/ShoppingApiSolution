using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Options;

namespace ShoppingApi.Services
{
    public class EFSqlProducts : ILookupProducts, IProductCommands
    {
        private readonly ShoppingDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly IOptions<ConfigurationForPricing> _pricingOptions;

        public EFSqlProducts(ShoppingDataContext context, IMapper mapper, MapperConfiguration mapperConfig, IOptions<ConfigurationForPricing> pricingOptions)
        {
            _context = context;
            _mapper = mapper;
            _mapperConfig = mapperConfig;
            _pricingOptions = pricingOptions;
        }

        public async Task<GetProductDetailResponse> Add(PostProductRequest productToAdd)
        {
            var product = _mapper.Map<Product>(productToAdd);
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == productToAdd.Category);
            if (category == null)
            {
                var newCategory = new Category { Name = productToAdd.Category };
                _context.Categories.Add(newCategory);
                product.Category = newCategory;
            }
            else
            {
                product.Category = category;
            }
            product.Price = productToAdd.Cost.Value * _pricingOptions.Value.Markup;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetProductDetailResponse>(product);

        }

        public async Task<GetProductDetailResponse> GetProductById(int id)
        {
            var response = await _context.Products
                .Where(p=> p.Id == id && p.RemovedFromInventory == false)
                .ProjectTo<GetProductDetailResponse>(_mapperConfig)
                .SingleOrDefaultAsync();
            return response;
            //return Task.FromResult( new GetProductDetailResponse
            //{
            //    Id = id,
            //    Name = "Some Product",
            //    Category = "Bread",
            //    Count = 1,
            //    Price = 1.89M
            //});
        }
    }
}
