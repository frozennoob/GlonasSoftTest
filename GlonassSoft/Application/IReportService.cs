using GlonassSoft.Models;
using System;
using System.Threading.Tasks;

namespace GlonassSoft.Application
{
    public interface IReportService
    {
        public Task<Guid> RequestReport(Guid userId);
        public Task<ReportInfo> GetReport(Guid requestGuid);
        public Task UpdatePercent();
        public Task Remove(Guid queryGuid);
    }
}
