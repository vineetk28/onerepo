using MantiScanServices.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Roles
{
    public class AnraRoleRepository : IRepository<Role>
    {
        private readonly MantiDbContext _context;

        public AnraRoleRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(Role item)
        {
            _context.MantiScanRole.Add(item);
            _context.SaveChanges();
        }

        public Role Find(long key)
        {
            return _context.MantiScanRole.FirstOrDefault(p => p.RoleId == key);
        }

        public Role GetNewItem()
        {
            return new Role();
        }

        public void Remove(long key)
        {
            var entity = _context.MantiScanRole.First(t => t.RoleId == key);
            _context.MantiScanRole.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Role item)
        {
            _context.MantiScanRole.Update(item);
            _context.SaveChanges();
        }

        public IQueryable<Role> GetAll(string emailId)
        {
            return _context.MantiScanRole;
        }

        public Role Find(string key)
        {
            return _context.MantiScanRole.FirstOrDefault(p => p.RoleName.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}
