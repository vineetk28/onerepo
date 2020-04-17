using MantiScanServices.Common;
using MantiScanServices.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Incidents
{
    public class IncidentRepository : IRepository<Incident>
    {
        private readonly MantiDbContext _context;

        public IncidentRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(Incident item)
        {
            _context.Incidents.Add(item);
            _context.SaveChanges();
        }

        public Incident Find(long key)
        {
            return _context.Incidents.Where(p => p.IncidentId == key).FirstOrDefault();
        }

        public Incident Find(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Incident> GetAll(string EmailId)
        {
            if (AnraCommon.IsAdmin(EmailId, _context) || string.IsNullOrEmpty(EmailId))
            {
                return _context.Incidents;
            }
            else
            {
                return _context.Incidents.Where(p => p.OrganizationId == _context.Users.Where(a => a.Email == EmailId).FirstOrDefault().OrganizationId);
            }
        }

        public Incident GetNewItem()
        {
            return new Incident();
        }

        public void Remove(long key)
        {
            var entity = _context.Incidents.First(t => t.IncidentId == key);
            _context.Incidents.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(Incident item)
        {
            _context.Incidents.Update(item);
            _context.SaveChanges();
        }
    }
}
