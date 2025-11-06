using Azure.Core;
using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface ICustomAuthorizationService
    {
        Task<IDataResult<JwtTokenDto>> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default);
    }
}
