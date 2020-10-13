using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.Models.Curbside
{
    public class PostCurbsideOrderRequest
    {
        [Required]
        public string For { get; set; }
        [Required]
        public string Items { get; set; }
    }
}
