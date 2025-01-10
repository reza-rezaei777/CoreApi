using Common.Utilities;
using CoreApi.DataLayer;
using CoreApi.Domin;
using CoreApi.Entities;
using Microsoft.EntityFrameworkCore;
namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new Exception("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }
        //public Task UpdateSecurityStampt(User user, CancellationToken cancellationToken)
        //{
        //    user.SecurityStamp = Guid.NewGuid();
        //    return base.UpdateAsync(user, cancellationToken);
        //}
        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken) 
        {
            user.LastLoginDate=DateTimeOffset.Now;
           return  base.UpdateAsync(user, cancellationToken, true);
        }
    }
}
