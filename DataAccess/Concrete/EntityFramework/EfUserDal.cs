using Core.DataAccess.EntityFramework;
using Core.Specifications;
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

        public async Task<List<UserDisplayDto>> GetAllUserAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.Users
                .Select(user => new UserDisplayDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<UserDisplayDto> GetUserAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
        {
            var result = await _context.Users.Where(specification.Criteria)
                .Select(user => new UserDisplayDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
