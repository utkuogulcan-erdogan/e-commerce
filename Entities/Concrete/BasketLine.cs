using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class BasketLine : IEntity
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }
        public static BasketLine CreateBasketLine(Guid basketId, Guid productId, int quantity)
        {
            return new BasketLine
            {
                Id = Guid.NewGuid(),
                BasketId = basketId,
                ProductId = productId,
                Quantity = quantity
            };

        }

        public static BasketLine UpdateQuantity(BasketLine basketLine, int quantity)
        {
            ArgumentNullException.ThrowIfNull(basketLine);
            basketLine.Quantity = quantity;
            return basketLine;
        }
    }
}
