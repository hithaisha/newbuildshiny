using FOF.Domain.Common;

namespace MORR.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? SKUCode { get; set; }
        public int CategoryId { get; set; }
        public bool? DisplayInFrontPage { get; set; }
        public decimal Price { get; set; }
        public bool IsShowWeb { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? ItemImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int Quantity { get; set; }


        public virtual Category Category { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }
}
