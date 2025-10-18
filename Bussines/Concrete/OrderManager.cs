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
using System.Transactions;

namespace Bussiness.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        IBasketDal _basketDal;
        IOrderAddressService _orderAddressService;
        IOrderPaymentDal _orderPaymentDal;
        IBasketLineDal _basketLineDal;
        public OrderManager(IOrderDal orderDal, IBasketDal basketDal, IOrderAddressService orderAddressService,IOrderPaymentDal orderPaymentDal, IBasketLineDal basketLineDal)
        {
            _orderDal = orderDal;
            _basketDal = basketDal;
            _orderAddressService = orderAddressService;
            _orderPaymentDal = orderPaymentDal;
            _basketLineDal = basketLineDal;
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
            var basket = await _basketDal.GetAsync(basket => basket.UserId == userId);
            if (basket == null || basket.BasketLines.Count() != 0)
                return new ErrorDataResult<OrderDisplayDto>("Basket is empty.");

            var order = Order.CreateOrder(userId, basket);

            if(dto.ShippingAddress == null)
            {
                return new ErrorResult("Shipping address is required.");
            }

            var shippingAddress = OrderAddress.CreateOrderAddress(order.Id, dto.ShippingAddress);
            order.AddOrderAddress(shippingAddress);

            if (dto.BillingAddress == null)
            {
                return new ErrorResult("Billing address is required.");
            }

            var billingAddress = OrderAddress.CreateOrderAddress(order.Id, dto.BillingAddress);
            order.AddOrderAddress(billingAddress);

            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _orderDal.AddAsync(order);
            await _basketLineDal.ClearBasketLinesAsync(basket.Id);
            scope.Complete();
            return new SuccessResult("Order created successfully.");
        }

        public async Task<IResult> UpdateOrderStatusAsync(Guid userId, OrderUpdateStatusDto dto)
        {
            var order = await _orderDal.GetAsync(o => o.Id == dto.OrderId);
            if (order == null)
                return new ErrorResult("Order not found.");
            if (order.UserId != userId)
                return new ErrorResult("Unauthorized access.");

            order = Order.UpdateOrderStatus(order, dto);
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
                order = Order.UpdateOrderStatus(order, new OrderUpdateStatusDto
                {
                    OrderId = order.Id,
                    Status = (int)OrderStatus.Failed,
                });
                await _orderDal.UpdateAsync(order);
                return new ErrorResult("Payment failed.");
            }

            var payment = OrderPayment.CreateOrderPayment(order.Id, dto);

            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _orderPaymentDal.AddAsync(payment);
            order = Order.UpdateOrderStatus(order, new OrderUpdateStatusDto
            {
                OrderId = order.Id,
                Status = (int)OrderStatus.Paid,
            });
            await _orderDal.UpdateAsync(order);
            scope.Complete();
            return new SuccessResult("Payment processed successfully.");
        }
    }
}
