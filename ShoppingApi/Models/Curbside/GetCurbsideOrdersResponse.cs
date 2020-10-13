using ShoppingApi.Domain;

namespace ShoppingApi.Models.Curbside
{
    public class GetCurbsideOrdersResponse : Collection<CurbsideOrder>
    {
        public int NumberOfPendingOrders { get; set; }
        public int NumberOfApprovedOrders { get; set; }
        public int NumberOfDeniedOrders { get; set; }
        public int NumberOfFulfilledOrders { get; set; }

    }
}
