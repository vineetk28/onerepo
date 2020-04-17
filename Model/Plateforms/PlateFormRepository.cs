using MantiScanServices.DataProvider;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Plateforms
{
    public class PlateFormRepository : IRepository<PlateForm>
    {
        private readonly MantiDbContext _context;
        private readonly ILogger _logger;

        public PlateFormRepository(MantiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("PostgreSqlProviderPath");
        }

        public IQueryable<PlateForm> GetAll(string EmailId)
        {
            return _context.PlateForms
            .Where(a => a.OrganizationId == _context.Users.Where(b => b.Email == EmailId).FirstOrDefault().OrganizationId);
        }

        public PlateForm Find(long key)
        {
            return _context.PlateForms.Where(p => p.PlateFormId == key).FirstOrDefault();                
        }

        public PlateForm Find(string gufi)
        {
            throw new NotImplementedException();
        }

        public void Remove(long key)
        {
            var entity = Find(key);
            _context.PlateForms.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Add(PlateForm item)
        {
            _context.PlateForms.Add(item);
            _context.SaveChanges();
        }

        public PlateForm GetNewItem()
        {
            return new PlateForm();
        }

        public void Update(PlateForm item)
        {
            _context.PlateForms.Update(item);
            _context.SaveChanges();
        }
    }
}
