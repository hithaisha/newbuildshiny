using FOF.Domain.Common;

namespace MORR.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        public string InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
