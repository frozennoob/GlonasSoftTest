using GlonassSoft.Application;
using GlonassSoft.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlonassSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserStatisticsController : ControllerBase
    {
        private readonly IUserStatisticsService _service;

        public UserStatisticsController(IUserStatisticsService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("report/user_statistics")]
        public async Task<Guid> RequestReport(Guid userId, DateTime dateFrom, DateTime dateTo)
        {
            var result = await _service.RequestReport(userId, dateFrom, dateTo);

            return result;
        }

        [HttpGet]
        [Route("report/info")]
        public async Task<ReportInfo> GetReport(Guid requestGuid)
        {
            var result = await _service.GetReport(requestGuid);

            return result;
        }
    }
}
