using MantiScanServices.DataProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Roles
{
    public class RoleRepository : IRepository<IdentityRole>
    {
        private readonly MantiDbContext _context;
        private readonly ILogger _logger;

        public RoleRepository(MantiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("PostgreSqlProviderPath");
        }

        public void Add(IdentityRole item)
        {
            throw new NotImplementedException();
        }

        public IdentityRole Find(long key)
        {
            throw new NotImplementedException();
        }

        public IdentityRole GetNewItem()
        {
            throw new NotImplementedException();
        }

        public void Remove(long key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(IdentityRole item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IdentityRole> GetAll(string emailId)
        {
            return _context.Roles;
        }

        public IdentityRole Find(string key)
        {
            return _context.Roles.FirstOrDefault(p => p.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
