using Bussiness.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Exceptions;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Specifications;
using Infrastructure.Persistence.Abstract;


namespace Bussiness.Concrete
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;
        IHashingHelper _hashingHelper;
        public UserManager(IUserRepository userRepository, IHashingHelper hashingHelper)
        {
            _userRepository = userRepository;
            _hashingHelper = hashingHelper;
        }

        public async Task<IResult> AddAsync(UserAddDto user, CancellationToken cancellationToken = default)
        {
            _hashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = User.Create(
                user,
                passwordHash,
                passwordSalt
            );
            await _userRepository.AddAsync(newUser, cancellationToken);
            return new SuccessResult("User added successfully.");
        }

        public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);
            return new SuccessResult("User deleted successfully.");
        }

        public async Task<IDataResult<List<UserDisplayDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetAllUserAsync(cancellationToken);
            return new SuccessDataResult<List<UserDisplayDto>>(response, "Users listed successfully.");
        }

        public async Task<IDataResult<UserDisplayDto>> GetByMailAsync(string email, CancellationToken cancellationToken = default)
        {
            var specification = new UserSpecification(email: email);
            return new SuccessDataResult<UserDisplayDto>(await _userRepository.GetUserAsync(specification, cancellationToken), "User retrieved successfully.");
        }

        public async Task<IResult> UpdateAsync(Guid id, UserUpdateDto user, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetAsync(u => u.Id == id, cancellationToken);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found.");
            }
            var userPasswordHash = existingUser.PasswordHash;
            var userPasswordSalt = existingUser.PasswordSalt;
            if (!string.IsNullOrEmpty(user.Password))
            {
                _hashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userPasswordHash = passwordHash;
                userPasswordSalt = passwordSalt;
            }
            var updatedUser = User.Update(existingUser, userPasswordHash, userPasswordSalt, user);
            await _userRepository.UpdateAsync(updatedUser, cancellationToken);
            return new SuccessResult("User updated successfully.");
        }
    }
}
