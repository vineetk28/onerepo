using MantiScanServices.DataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Users
{
    public class UserRepository : IRepository<User>
    {
        private readonly ILogger _logger;
        private readonly MantiDbContext _context;

        public UserRepository(MantiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("PostgreSqlProviderPath");
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public User Find(long key)
        {
            throw new NotImplementedException();
        }

        public User Find(string key)
        {   
            return _context.Users.FirstOrDefault(p => p.Id == key && !p.IsDeleted);
        }

        public IQueryable<User> GetAll(string EmailId)
        {
            // User u;
            var roles = from u in _context.Users
                            //            join ur in _context.UserRoleDetails on u.Id equals ur.UserId
                            //            join r in _context.AnraRoles on ur.RoleId equals r.RoleId
                            //            where u.Email == EmailId
                            //select r;
                        select u;

            return _context.Users.Include(a => a.Roles).Where(u => u.Email != EmailId && !u.IsDeleted);//temporary
        }

        public User GetNewItem()
        {
            return new User();
        }

        public void Remove(long key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            var entity = _context.Users
                .Include(x => x.Roles)                
                .First(t => t.Id == key);

            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Users.Update(item);
            _context.SaveChanges();
        }
    }
}
