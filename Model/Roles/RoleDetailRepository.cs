using MantiScanServices.DataProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MantiScanServices.Model.Roles
{
    public class RoleDetailRepository : IRepository<RoleDetail>
    {
        private readonly MantiDbContext _context;

        public RoleDetailRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(RoleDetail item)
        {
            _context.RoleDetails.Add(item);
            _context.SaveChanges();
        }

        public RoleDetail Find(string key)
        {
            throw new NotImplementedException();
        }

        public RoleDetail Find(long key)
        {
            return _context.RoleDetails.FirstOrDefault(p => p.RoleDetailId == key);
        }

        public IQueryable<RoleDetail> GetAll(string emailId)
        {
            return _context.RoleDetails.Include(v => v.Module).Include(v => v.Role);
        }

        public RoleDetail GetNewItem()
        {
            return new RoleDetail();
        }

        public void Remove(long key)
        {
            var entity = _context.RoleDetails.First(t => t.RoleDetailId == key);
            _context.RoleDetails.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(RoleDetail item)
        {
            _context.RoleDetails.Update(item);
            _context.SaveChanges();
        }
    }
}
