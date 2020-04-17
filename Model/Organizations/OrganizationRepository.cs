using MantiScanServices.Common;
using MantiScanServices.DataProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Organizations
{
    public class OrganizationRepository : IRepository<Organization>
    {
        private readonly MantiDbContext _context;

        public OrganizationRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(Organization item)
        {
            _context.Organizations.Add(item);
            _context.SaveChanges();
        }

        public Organization Find(string key)
        {
            throw new NotImplementedException();
        }

        public Organization Find(long key)
        {
            return _context.Organizations.FirstOrDefault(p => p.OrganizationId == key);
        }

        public IQueryable<Organization> GetAll(string emailId)
        {

            if (AnraCommon.IsAdmin(emailId, _context) || string.IsNullOrEmpty(emailId))
            {
                return _context.Organizations;
            }
            else
            {
                return _context.Organizations.Where(o => o.OrganizationId == _context.Users.FirstOrDefault(u => u.Email == emailId).OrganizationId);
            }
        }

        public Organization GetNewItem()
        {
            return new Organization();
        }

        public void Remove(long key)
        {
            var entity = _context.Organizations
                .Include(x => x.Users)                
                .Include("Users.Roles")                
                .Include(x => x.Incidents)                
                .First(t => t.OrganizationId == key);

            _context.Organizations.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(Organization item)
        {
            _context.Organizations.Update(item);
            _context.SaveChanges();
        }
    }
}
