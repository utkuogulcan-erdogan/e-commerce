using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class JwtTokenDto : IDto
    {
        public string Token { get; set; }
    }
}
