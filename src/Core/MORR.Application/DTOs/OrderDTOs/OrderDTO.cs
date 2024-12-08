namespace MORR.Application.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderItems = new List<OrderItemDTO>();
        }
        public string? InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public decimal? TotalPrice { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }


        public string ItemName { get; set; }
        public string ItemImageUrl { get; set; }
    }
}
