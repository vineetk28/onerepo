using MantiScanServices.DataProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Users
{
    public class UserRoleDetailRepository : IRepository<UserRoleDetail>
    {
        private readonly MantiDbContext _context;

        public UserRoleDetailRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(UserRoleDetail item)
        {
            _context.UserRoleDetails.Add(item);
            _context.SaveChanges();
        }

        public UserRoleDetail Find(string key)
        {
            throw new NotImplementedException();
        }

        public UserRoleDetail Find(long key)
        {
            return _context.UserRoleDetails.FirstOrDefault(p => p.UserRoleDetailId == key);
        }

        public IQueryable<UserRoleDetail> GetAll(string emailId)
        {
            return _context.UserRoleDetails.Include(v => v.Role).Include(v => v.User);
        }

        public UserRoleDetail GetNewItem()
        {
            return new UserRoleDetail();
        }

        public void Remove(long key)
        {
            var entity = _context.UserRoleDetails.First(t => t.UserRoleDetailId == key);
            _context.UserRoleDetails.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(UserRoleDetail item)
        {
            _context.UserRoleDetails.Update(item);
            _context.SaveChanges();
        }
    }
}
