using Bussiness.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class AuthorizationManager : ICustomAuthorizationService
    {
        private readonly JwtHelper _jwtHelper;
        IUserDal _userDal;
        IHashingHelper _hashingHelper;

        public AuthorizationManager(IUserDal userDal, IHashingHelper hashingHelper, JwtHelper jwtHelper)
        {
            _userDal = userDal;
            _hashingHelper = hashingHelper;
            _jwtHelper = jwtHelper;
        }

        public async Task<IDataResult<JwtTokenDto>> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default)
        {
            User user = await _userDal.GetAsync(u => u.Email == loginRequestDto.Email, cancellationToken);
            if (user == null)
                throw new Exception("User not found");
            if (!_hashingHelper.VerifyPasswordHash(loginRequestDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Password is incorrect");

            string jwtToken = _jwtHelper.GenerateToken(user.Id, user.Email);
            JwtTokenDto jwtTokenDto = new JwtTokenDto
            {
                Token = jwtToken,
            };

            return new SuccessDataResult<JwtTokenDto>(jwtTokenDto);
        }
    }
}
