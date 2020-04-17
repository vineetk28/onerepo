using MantiScanServices.DataProvider;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.RolePrivilege
{
    public class RolePrivilegeRepository : IRepository<RolePrivilege>
    {
        private readonly MantiDbContext _context;
        private readonly ILogger _logger;

        public RolePrivilegeRepository(MantiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("PostgreSqlProviderPath");
        }

        public void Add(RolePrivilege item)
        {
            _context.RolePrivilege.Add(item);
            _context.SaveChanges();
        }

        public RolePrivilege Find(string key)
        {
            throw new NotImplementedException();
        }

        public RolePrivilege Find(long key)
        {
            return _context.RolePrivilege.FirstOrDefault(p => p.RolePrivilegeId == key);
        }

        public IQueryable<RolePrivilege> GetAll(string emailId)
        {
            return _context.RolePrivilege;
        }

        public RolePrivilege GetNewItem()
        {
            return new RolePrivilege();
        }

        public void Remove(long key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            var entity = _context.Users.First(t => t.Id == key);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(RolePrivilege item)
        {
            _context.RolePrivilege.Update(item);
            _context.SaveChanges();
        }
    }
}
