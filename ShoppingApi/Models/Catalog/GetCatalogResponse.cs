using System.Collections.Generic;

namespace ShoppingApi.Models.Catalog
{
    public class GetCatalogResponse
    {
        public List<GetCatalogResponseSummaryItem> Data { get; set; }
    }

    public class GetCatalogResponseSummaryItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
