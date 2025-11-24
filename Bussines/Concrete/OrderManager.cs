using Bussiness.Abstract;
using Core.Utilities.Results;
using Core.Exceptions;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Enums;
using Infrastructure.Persistence.Abstract;
using System.Transactions;

namespace Bussiness.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderRepository _orderRepository;
        IBasketRepository _basketRepository;
        IOrderAddressService _orderAddressService;
        IOrderPaymentRepository _orderPaymentRepository;
        public OrderManager(IOrderRepository orderRepository, IBasketRepository basketRepository, IOrderAddressService orderAddressService, IOrderPaymentRepository orderPaymentRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _orderAddressService = orderAddressService;
            _orderPaymentRepository = orderPaymentRepository;
        }
        public async Task<IDataResult<List<OrderDisplayDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return new SuccessDataResult<List<OrderDisplayDto>>(await _orderRepository.GetAllOrdersAsync(cancellationToken), "Orders listed successfully.");
        }

        public async Task<IDataResult<OrderDisplayDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return new SuccessDataResult<OrderDisplayDto>(await _orderRepository.GetOrderByIdAsync(id, cancellationToken), "Order retrieved successfully.");
        }

        public async Task<IDataResult<List<OrderDisplayDto>>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return new SuccessDataResult<List<OrderDisplayDto>>(await _orderRepository.GetOrdersByUserIdAsync(userId, cancellationToken), "User orders listed successfully.");
        }

        public async Task<IResult> CreateOrderAsync(Guid userId, OrderCreateDto dto, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetDetailedBasketByUserIdAsync(userId, cancellationToken);
            if (basket == null || !basket.BasketLines.Any())
                throw new BadRequestException("Basket is empty.");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                OrderStatus = (int)OrderStatus.ReadyForPayment,
                TotalAmount = basket.BasketLines.Sum(bl => bl.Quantity * bl.Product.Price),
                OrderLines = basket.BasketLines.Select(bl => new OrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = bl.ProductId,
                    ProductName = bl.Product.Name,
                    Quantity = bl.Quantity,
                    UnitPrice = bl.Product.Price,
                }).ToList()
            };

            if (dto.ShippingAddress != null)
            {
                order.OrderAddresses = new List<OrderAddress>
                    {
                        new OrderAddress
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            AddressType = AddressType.Shipping.ToString(),
                            Street = dto.ShippingAddress.Street,
                            City = dto.ShippingAddress.City,
                            Country = dto.ShippingAddress.Country,
                            PostalCode = dto.ShippingAddress.PostalCode,
                        }
                    };
            }
            else
            {
                throw new BadRequestException("Shipping address is required.");
            }
            if (dto.BillingAddress != null)
            {
                if (order.OrderAddresses == null)
                    order.OrderAddresses = new List<OrderAddress>();
                order.OrderAddresses.Add(new OrderAddress
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    AddressType = AddressType.Billing.ToString(),
                    Street = dto.BillingAddress.Street,
                    City = dto.BillingAddress.City,
                    Country = dto.BillingAddress.Country,
                    PostalCode = dto.BillingAddress.PostalCode,
                });
            }
            else
            {
                throw new BadRequestException("Billing address is required.");
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _orderRepository.AddAsync(order, cancellationToken);
                await _basketRepository.DeleteAsync(basket.Id, cancellationToken);
                scope.Complete();
            }
            return new SuccessResult("Order created successfully.");
        }

        public async Task<IResult> UpdateOrderStatusAsync(Guid userId, OrderUpdateStatusDto dto, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetOrderByIdAsync(dto.OrderId, cancellationToken);
            if (order == null)
                throw new NotFoundException("Order not found.");

            var orderEntity = await _orderRepository.GetAsync(o => o.Id == dto.OrderId, cancellationToken);
            if (orderEntity == null || orderEntity.UserId != userId)
                throw new UnauthorizedException("Order not found or unauthorized.");

            orderEntity.OrderStatus = dto.Status;
            await _orderRepository.UpdateAsync(orderEntity, cancellationToken);
            return new SuccessResult("Order status updated successfully.");
        }

        public async Task<IResult> CreatePaymentAsync(Guid userId, OrderPaymentDto dto, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetAsync(o => o.Id == dto.OrderId, cancellationToken);
            if (order == null || order.UserId != userId)
                throw new NotFoundException("Order not found.");

            if (order.OrderStatus != (int)OrderStatus.ReadyForPayment)
                throw new BadRequestException("Order is not ready for payment.");

            if ((PaymentStatus)dto.Status != PaymentStatus.Completed)
            {
                order.OrderStatus = (int)OrderStatus.Failed;
                await _orderRepository.UpdateAsync(order, cancellationToken);
                throw new BadRequestException("Payment failed.");
            }

            var payment = new OrderPayment
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Amount = order.TotalAmount,
                Provider = dto.Provider,
                TransactionId = dto.TransactionId,
                Status = (int)PaymentStatus.Completed,
                CreatedAt = DateTime.UtcNow
            };
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _orderPaymentRepository.AddAsync(payment, cancellationToken);
                order.OrderStatus = (int)OrderStatus.Completed;
                await _orderRepository.UpdateAsync(order, cancellationToken);
                scope.Complete();
            }

            return new SuccessResult("Payment processed successfully.");
        }
    }
}
