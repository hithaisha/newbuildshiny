using MediatR;
using MORR.Application.Common.Helpers;
using MORR.Application.Common.Interfaces;
using MORR.Application.DTOs.OrderDTOs;
using MORR.Domain.Entities;
using MORR.Domain.Repositories.Command;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Orders.Commands.SaveOrder
{
    public record SaveOrderCommand(OrderDTO order) : IRequest<OrderDTO>
    {
    }

    public class SaveOrderCommandHandler : IRequestHandler<SaveOrderCommand, OrderDTO>
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        public SaveOrderCommandHandler
        (
            IOrderCommandRepository orderCommandRepository,
            IOrderQueryRepository orderQueryRepository,
            IProductQueryRepository productQueryRepository,
            IProductCommandRepository productCommandRepository,
            ICurrentUserService currentUserService,
            IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository
        )
        {
            this._orderQueryRepository = orderQueryRepository;
            this._orderCommandRepository = orderCommandRepository;
            this._productQueryRepository = productQueryRepository;
            this._productCommandRepository = productCommandRepository;
            this._currentUserService = currentUserService;
            this._userQueryRepository = userQueryRepository;
            this._userCommandRepository = userCommandRepository;    
        }
        public async Task<OrderDTO> Handle(SaveOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryRepository.GetById(request.order.OrderId, cancellationToken);
            var loggedInUser = await _userQueryRepository.GetById(_currentUserService.UserId.Value, cancellationToken);

            if(order is null)
            {
                order = new Domain.Entities.Order();

                order.InvoiceNumber = InvoiceNumberHelper.GenerateInvoiceNumber();
                order.TotalPrice = request.order.TotalPrice ?? 0;

                foreach(var orderItem in request.order.OrderItems)
                {
                    order.OrderItems.Add(new Domain.Entities.OrderItem()
                    {
                        ProductId = orderItem.ProductId,
                        Quantity = orderItem.Quantity,
                        TotalPrice = orderItem.TotalPrice,
                    });
                    
                    var product = await _productQueryRepository.GetById(orderItem.ProductId, cancellationToken);

                    product.Quantity = product.Quantity - orderItem.Quantity;

                    await _productCommandRepository.UpdateAsync(product, cancellationToken);

                    loggedInUser.LoyaltyPoints = (int)(loggedInUser.LoyaltyPoints + product.LoyaltyPoints);

                    await _userCommandRepository.UpdateAsync(loggedInUser, cancellationToken);
                    
                }

                var newOrder = await _orderCommandRepository.AddAsync(order, cancellationToken);

                var responseOrderDto = MapToOrderDTO(newOrder);

                return responseOrderDto;
            }
            else
            {
                return new OrderDTO();
            }
        }

        private OrderDTO MapToOrderDTO(Order order)
        {
            var orderDto = new OrderDTO();

            orderDto.OrderId = order.OrderId;
            orderDto.InvoiceNumber = order.InvoiceNumber;
            orderDto.TotalPrice = order.TotalPrice;

            foreach(var orderItem in order.OrderItems)
            {
                orderDto.OrderItems.Add(new OrderItemDTO()
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    TotalPrice = orderItem.TotalPrice,
                    ItemName = orderItem.Product.ItemName ?? string.Empty,
                    ItemImageUrl = orderItem.Product.ItemImageUrl ?? string.Empty,

                });
            }

            return orderDto;
        }
    }
}
