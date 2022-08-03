using GlonassSoft.DAL;
using GlonassSoft.Infrastructure;
using GlonassSoft.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GlonassSoft.Application
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repo;
        private readonly IConfiguration _conf;

        public ReportService(IReportRepository repo, IConfiguration conf)
        {
            _repo = repo;
            _conf = conf;
        }

        public async Task<ReportInfo> GetReport(Guid requestGuid)
        {
            var result = new ReportInfo();
            var query = await _repo.Get(requestGuid);
            if (query != null)
            {
                result.Percent = query.Percent;
                result.Query = query.QueryId;
                if (query.UserId != null)
                {
                    result.Result = new ReportResult
                    {
                        UserId = query.UserId,
                        CountSignIn = query.CountSignIn.ToString()
                    };
                }
            }

            return result;
        }

        public async Task<Guid> RequestReport(Guid userId)
        {
            var query = new Query
            {
                QueryId = new Guid(),
                CountSignIn = 0,
                CreatedTime = DateTime.Now,
                Percent = 0,
                UserId = userId
            };
            var queryGuid = await _repo.Create(query);

            return queryGuid;
        }

        public async Task UpdatePercent()
        {
            var delay = _conf.GetValue<int>("DefaultQueryDelay");
            var frequency = _conf.GetValue<int>("RefreshFrequncy");
            var koef = (double)frequency / delay * 100;
            var queries = await _repo.GetAll();
            foreach(var query in queries)
            {
                if (query.Percent <= 100)
                {
                    query.Percent += (byte)koef;
                    if (query.Percent > 100)
                    {
                        query.Percent = 100;
                    }
                    await _repo.Update(query.QueryId, query);
                }
            }
        }
    }
}
