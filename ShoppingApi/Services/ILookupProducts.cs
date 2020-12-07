using ShoppingApi.Models.Products;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public interface ILookupProducts
    {
        Task<GetProductDetailResponse> GetProductById(int id);
    }
}