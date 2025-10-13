using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }   
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Basket Basket { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
