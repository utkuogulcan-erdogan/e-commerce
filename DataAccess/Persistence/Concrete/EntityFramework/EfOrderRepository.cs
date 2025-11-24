using Core.DataAccess.EntityFramework;
using Core.Specifications;
using Entities.Concrete;
using Entities.DTO_s;
using Infrastructure.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Concrete.EntityFramework
{
    public class EfOrderRepository : EfEntityRepositoryBase<Order, MyShopContext>, IOrderRepository
    {
        public EfOrderRepository(MyShopContext context) : base(context)
        {
        }

        public async Task<List<OrderDisplayDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
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
                .ToListAsync(cancellationToken);
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

        public async Task<OrderDisplayDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(o => o.Id == id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<OrderDisplayDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
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
                .ToListAsync(cancellationToken);
        }
    }
}
