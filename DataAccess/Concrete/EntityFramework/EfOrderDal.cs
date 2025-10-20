using Core.DataAccess.EntityFramework;
using Core.Specifications;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, MyShopContext>, IOrderDal
    {
        public EfOrderDal(MyShopContext context) : base(context)
        {
        }

        public async Task<List<OrderDisplayDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Select(order => new OrderDisplayDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    TotalAmount = order.TotalAmount,
                    CreatedAt = order.CreatedAt,

                    OrderLines = order.OrderLines.Select(ol => new OrderLineDisplayDto
                    {
                        Id = ol.Id,
                        OrderId = ol.OrderId,
                        Quantity = ol.Quantity,
                        UnitPrice = ol.UnitPrice,
                        TotalPrice = ol.LineTotal,
                        Product = new ProductDisplayDto
                        {
                            Id = ol.Product.Id,
                            Name = ol.Product.Name,
                            Description = ol.Product.Description,
                            Price = ol.Product.Price,
                            Stock = ol.Product.Stock
                        }
                    }).ToList(),

                    OrderShippingAddress = order.OrderAddresses
                        .Where(a => a.AddressType == "shipping")
                        .Select(a => new OrderAddressDisplayDto
                        {
                            Id = a.Id,
                            Street = a.Street,
                            City = a.City,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        }).FirstOrDefault(),

                    OrderBillingAddress = order.OrderAddresses
                        .Where(a => a.AddressType == "billing")
                        .Select(a => new OrderAddressDisplayDto
                        {
                            Id = a.Id,
                            Street = a.Street,
                            City = a.City,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        }).FirstOrDefault(),

                    OrderPayment = order.OrderPayment == null ? null : new OrderPaymentDisplayDto
                    {
                        Id = order.OrderPayment.Id,
                        PaymentMethod = order.OrderPayment.Provider,
                        PaymentStatus = order.OrderPayment.Status,
                        Amount = order.OrderPayment.Amount,
                        PaymentDate = order.OrderPayment.CreatedAt
                    }
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<OrderDisplayDto> GetOrderAsync(ISpecification<Order> specification)
        {
            return await _context.Orders
                .Where(specification.Criteria)
                .Select(order => new OrderDisplayDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    TotalAmount = order.TotalAmount,
                    CreatedAt = order.CreatedAt,
                    OrderLines = order.OrderLines.Select(ol => new OrderLineDisplayDto
                    {
                        Id = ol.Id,
                        OrderId = ol.OrderId,
                        Quantity = ol.Quantity,
                        UnitPrice = ol.UnitPrice,
                        TotalPrice = ol.LineTotal
                    }).ToList(),
                    OrderShippingAddress = order.OrderAddresses
                        .Where(a => a.AddressType == "shipping")
                        .Select(a => new OrderAddressDisplayDto
                        {
                            Id = a.Id,
                            Street = a.Street,
                            City = a.City,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        }).FirstOrDefault(),
                    OrderBillingAddress = order.OrderAddresses
                        .Where(a => a.AddressType == "billing")
                        .Select(a => new OrderAddressDisplayDto
                        {
                            Id = a.Id,
                            Street = a.Street,
                            City = a.City,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        }).FirstOrDefault(),
                    OrderPayment = order.OrderPayment == null ? null : new OrderPaymentDisplayDto
                    {
                        Id = order.OrderPayment.Id,
                        PaymentMethod = order.OrderPayment.Provider,
                        PaymentStatus = order.OrderPayment.Status,
                        Amount = order.OrderPayment.Amount,
                        PaymentDate = order.OrderPayment.CreatedAt
                    }
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrderDisplayDto>> GetOrdersAsync(ISpecification<Order> specification)
        {
            return await _context.Orders
                .Where(specification.Criteria)
                .Select(order => new OrderDisplayDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    TotalAmount = order.TotalAmount,
                    CreatedAt = order.CreatedAt,
                    OrderLines = order.OrderLines.Select(ol => new OrderLineDisplayDto
                    {
                        Id = ol.Id,
                        OrderId = ol.OrderId,
                        Quantity = ol.Quantity,
                        UnitPrice = ol.UnitPrice,
                        TotalPrice = ol.LineTotal
                    }).ToList(),
                    OrderShippingAddress = order.OrderAddresses
                        .Where(a => a.AddressType == "shipping")
                        .Select(a => new OrderAddressDisplayDto
                        {
                            Id = a.Id,
                            Street = a.Street,
                            City = a.City,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        }).FirstOrDefault(),
                    OrderBillingAddress = order.OrderAddresses
                        .Where(a => a.AddressType == "billing")
                        .Select(a => new OrderAddressDisplayDto
                        {
                            Id = a.Id,
                            Street = a.Street,
                            City = a.City,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        }).FirstOrDefault(),
                    OrderPayment = order.OrderPayment == null ? null : new OrderPaymentDisplayDto
                    {
                        Id = order.OrderPayment.Id,
                        PaymentMethod = order.OrderPayment.Provider,
                        PaymentStatus = order.OrderPayment.Status,
                        Amount = order.OrderPayment.Amount,
                        PaymentDate = order.OrderPayment.CreatedAt
                    }
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
