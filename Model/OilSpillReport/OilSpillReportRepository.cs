using MantiScanServices.DataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MantiScanServices.Model.OilSpillReport
{
    public class OilSpillReportRepository : IRepository<OilSpillReport>
    {
        private readonly MantiDbContext _context;
        private readonly ILogger _logger;

        public OilSpillReportRepository(MantiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("PostgreSqlProviderPath");
        }

        public IQueryable<OilSpillReport> GetAll(string EmailId)
        {
            return _context.OilSpillReports
                .Include(r => r.NotificationCompanys)
                .Include(r => r.NotificationAgencys)
            .Where(a=>a.OrganizationId==_context.Users.Where(b=>b.Email == EmailId).FirstOrDefault().OrganizationId);
        }

        public OilSpillReport Find(long key)
        {
            return _context.OilSpillReports
                .Include(r => r.NotificationCompanys)
                .Include(r => r.NotificationAgencys)
                .FirstOrDefault(p => p.OilSpillReportId == key);
        }

        public OilSpillReport Find(string gufi)
        {
            return _context.OilSpillReports
                 .Include(r => r.NotificationCompanys)
                .Include(r => r.NotificationAgencys)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void Remove(long key)
        {
            var entity = Find(key);
            _context.OilSpillReports.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(string key)
        {
            var entity = Find(key);
            _context.OilSpillReports.Remove(entity);
            _context.SaveChanges();
        }

        public void Add(OilSpillReport item)
        {
            _context.OilSpillReports.Add(item);
            _context.SaveChanges();
        }

        public OilSpillReport GetNewItem()
        {
            return new OilSpillReport();
        }

        public void Update(OilSpillReport item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
