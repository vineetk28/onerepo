using MantiScanServices.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Modules
{
    public class ModuleRepository : IRepository<Module>
    {
        private readonly MantiDbContext _context;

        public ModuleRepository(MantiDbContext context)
        {
            _context = context;
        }

        public void Add(Module item)
        {
            _context.Modules.Add(item);
            _context.SaveChanges();
        }

        public Module Find(string key)
        {
            throw new NotImplementedException();
        }

        public Module Find(long key)
        {
            return _context.Modules.FirstOrDefault(p => p.ModuleId == key);
        }

        public IQueryable<Module> GetAll(string emailId)
        {
            return _context.Modules;
        }

        public Module GetNewItem()
        {
            return new Module();
        }

        public void Remove(long key)
        {
            var entity = _context.Modules.First(t => t.ModuleId == key);
            _context.Modules.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(Module item)
        {
            _context.Modules.Update(item);
            _context.SaveChanges();
        }
    }
}
