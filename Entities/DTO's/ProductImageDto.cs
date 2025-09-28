using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
}
