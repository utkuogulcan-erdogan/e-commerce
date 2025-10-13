using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        IBasketDal _basketDal;
        IOrderAddressService _orderAddressService;
        IOrderPaymentDal _orderPaymentDal;
        public OrderManager(IOrderDal orderDal, IBasketDal basketDal, IOrderAddressService orderAddressService,IOrderPaymentDal orderPaymentDal)
        {
            _orderDal = orderDal;
            _basketDal = basketDal;
            _orderAddressService = orderAddressService;
            _orderPaymentDal = orderPaymentDal;
        }
        public async Task<IDataResult<List<OrderDisplayDto>>> GetAllAsync()
        {
            return new SuccessDataResult<List<OrderDisplayDto>>(await _orderDal.GetAllOrdersAsync(), "Orders listed successfully.");
        }

        public async Task<IDataResult<OrderDisplayDto>> GetOrderByIdAsync(Guid id)
        {
            return new SuccessDataResult<OrderDisplayDto>(await _orderDal.GetOrderByIdAsync(id), "Order retrieved successfully.");
        }

        public async Task<IDataResult<List<OrderDisplayDto>>> GetOrdersByUserIdAsync(Guid userId)
        {
            return new SuccessDataResult<List<OrderDisplayDto>>(await _orderDal.GetOrdersByUserIdAsync(userId), "User orders listed successfully.");
        }

        public async Task<IResult> CreateOrderAsync(Guid userId, OrderCreateDto dto)
        {
            var basket = await _basketDal.GetDetailedBasketByUserIdAsync(userId);
            if (basket == null || !basket.BasketLines.Any())
                return new ErrorDataResult<OrderDisplayDto>("Basket is empty.");

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
                return new ErrorResult("Shipping address is required.");
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
                return new ErrorResult("Billing address is required.");
            }
            await _orderDal.AddAsync(order);
            await _basketDal.DeleteAsync(basket.Id);
            return new SuccessResult("Order created successfully.");
        }

        public async Task<IResult> UpdateOrderStatusAsync(Guid userId, OrderUpdateStatusDto dto)
        {
            var order = await _orderDal.GetAsync(o => o.Id == dto.OrderId);
            if (order == null)
                return new ErrorResult("Order not found.");
            if (order.UserId != userId)
                return new ErrorResult("Unauthorized access.");

            order.OrderStatus = dto.Status;
            await _orderDal.UpdateAsync(order);
            return new SuccessResult("Order status updated successfully.");
        }

        public async Task<IResult> CreatePaymentAsync(Guid userId, OrderPaymentDto dto)
        {
            var order = await _orderDal.GetAsync(o => o.Id == dto.OrderId);
            if (order == null || order.UserId != userId)
                return new ErrorResult("Order not found.");

            if (order.OrderStatus != (int)OrderStatus.ReadyForPayment)
                return new ErrorResult("Order is not ready for payment.");

            if ((PaymentStatus)dto.Status != PaymentStatus.Completed)
            {
                order.OrderStatus = (int)OrderStatus.Failed;
                await _orderDal.UpdateAsync(order);
                return new ErrorResult("Payment failed.");
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

            await _orderPaymentDal.AddAsync(payment);
            order.OrderStatus = (int)OrderStatus.Completed;
            await _orderDal.UpdateAsync(order);

            return new SuccessResult("Payment processed successfully.");
        }
    }
}
