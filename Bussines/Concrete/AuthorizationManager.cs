using Bussiness.Abstract;
using Core.Exceptions;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.DTO_s;
using Infrastructure.Persistence.Abstract;
namespace Bussiness.Concrete
{
    public class AuthorizationManager : ICustomAuthorizationService
    {
        private readonly JwtHelper _jwtHelper;
        IUserRepository _userRepository;
        IHashingHelper _hashingHelper;

        public AuthorizationManager(IUserRepository userRepository, IHashingHelper hashingHelper, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _hashingHelper = hashingHelper;
            _jwtHelper = jwtHelper;
        }

        public async Task<IDataResult<JwtTokenDto>> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default)
        {
            User user = await _userRepository.GetAsync(u => u.Email == loginRequestDto.Email, cancellationToken);
            if (user == null)
                throw new NotFoundException("User not found with the given email");
            if (!_hashingHelper.VerifyPasswordHash(loginRequestDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedException("Password is incorrect");

            string jwtToken = _jwtHelper.GenerateToken(user.Id, user.Email);
            JwtTokenDto jwtTokenDto = new JwtTokenDto
            {
                Token = jwtToken,
            };

            return new SuccessDataResult<JwtTokenDto>(jwtTokenDto);
        }
    }
}
