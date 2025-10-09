using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, MyShopContext>, IUserDal
    {
        public EfUserDal(MyShopContext context) : base(context)
        {
        }

        public async Task<List<UserDisplayDto>> GetAllUserAsync()
        {
            var result = await (
                from user in _context.Users
                select new UserDisplayDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                }
            ).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<UserDisplayDto> GetUserAsync(string email)
        {
            var result = await (
                from user in _context.Users
                where user.Email == email
                select new UserDisplayDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                }
            ).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task AddUserAsync(UserAddDto user, byte[] passwordHash)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
