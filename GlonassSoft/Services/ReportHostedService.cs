using GlonassSoft.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlonassSoft.Services
{
    public class ReportHostedService : IHostedService
    {
        private readonly IReportService _service;
        private readonly IConfiguration _conf;
        private Timer _timer;

        public ReportHostedService(IServiceScopeFactory serviceScopeFactory, IConfiguration conf)
        {
            var serviceProvider = serviceScopeFactory
                .CreateScope()
                .ServiceProvider;
            _service = serviceProvider.GetRequiredService<IReportService>();
            _conf = conf;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var frequency = _conf.GetValue<int>("RefreshFrequncy");
            _timer = new Timer(CalculatePercents, null, TimeSpan.FromSeconds(frequency), TimeSpan.FromSeconds(frequency));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void CalculatePercents(object o)
        {
            await _service.UpdatePercent();
        }
    }
}
