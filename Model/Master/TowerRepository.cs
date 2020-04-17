using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantiScanServices.Common;
using MantiScanServices.DataProvider;

namespace MantiScanServices.Model.Master
{
    public class TowerRepository : IRepository<Tower>
    {
        private readonly MantiDbContext _context;

        public TowerRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(Tower item)
        {
            _context.Towers.Add(item);
            _context.SaveChanges();
        }

        public Tower Find(long key)
        {
            return _context.Towers.Where(p => p.TowerId == key).FirstOrDefault();
        }

        public Tower Find(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tower> GetAll(string emailId)
        {
            if (AnraCommon.IsAdmin(emailId, _context) || string.IsNullOrEmpty(emailId))
            {
                return _context.Towers;
            }
            else
            {
                return _context.Towers.Where(p => p.UserId == _context.Users.FirstOrDefault(a=>a.Email==emailId).Id);
            }
        }

        public Tower GetNewItem()
        {
            return new Tower();
        }

        public void Remove(long key)
        {
            var entity = _context.Towers.First(t => t.TowerId == key);
            _context.Towers.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(Tower item)
        {
            _context.Towers.Update(item);
            _context.SaveChanges();
        }

    }
}
