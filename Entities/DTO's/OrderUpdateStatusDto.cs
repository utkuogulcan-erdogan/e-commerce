using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class OrderUpdateStatusDto : IDto
    {
        public Guid OrderId { get; set; }
        public int Status { get; set; }
    }
}
