using GlonassSoft.Application;
using GlonassSoft.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GlonassSoft.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("user_statistics")]
        public async Task<Guid> RequestReport([FromQuery]Guid userId)
        {
            var result = await _service.RequestReport(userId);

            return result;
        }

        [HttpGet]
        [Route("info")]
        public async Task<ReportInfo> GetReport(Guid requestGuid)
        {
            var result = await _service.GetReport(requestGuid);

            return result;
        }
    }
}
