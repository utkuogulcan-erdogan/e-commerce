using Core.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Basket : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public User User { get; set; }
        public ICollection<BasketLine> BasketLines { get; set; }

        public static Basket Create(Guid userId)
        {
            return new Basket
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                BasketLines = new List<BasketLine>()
            };
        }
    }
}
