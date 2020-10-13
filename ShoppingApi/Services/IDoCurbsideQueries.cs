using ShoppingApi.Domain;
using ShoppingApi.Models.Curbside;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    public interface IDoCurbsideQueries
    {
        Task<GetCurbsideOrdersResponse> GetAll();
        Task<CurbsideOrder> GetById(int orderId);
    }
}