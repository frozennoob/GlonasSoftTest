using GlonassSoft.Application;
using GlonassSoft.DAL;
using GlonassSoft.Infrastructure;
using GlonassSoft.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlonassSoft.Tests
{
    [TestClass]
    public class ReportServiceTests
    {
        private readonly IConfiguration _conf;
        private readonly Guid _queryGuid;
        private readonly Guid _userGuid;
        private readonly Query _query;
        private readonly int _queryDelay;
        private readonly int _frequency;

        public ReportServiceTests()
        {
            _queryDelay = 60;
            _frequency = 10;

            var inMemorySettings = new Dictionary<string, string> {
                {"DefaultQueryDelay", $"{_queryDelay}"},
                {"RefreshFrequncy", $"{_frequency}"},
            };

            _conf = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _queryGuid = new Guid("1a98b57d-e090-4d18-8654-678e463b73e8");
            _userGuid = new Guid("b28d0ced-8af5-4c94-8650-c7946241fd1a");
            _query = new Query
            {
                QueryId = _queryGuid,
                UserId = _userGuid,
                CountSignIn = 1,
                Percent = 10
            };
        }

        [TestMethod]
        public async Task CanReturnValidRequest()
        {     
            // Arrange
            var mock = new Mock<IReportRepository>();
            mock.Setup(p => p.Get(_queryGuid)).ReturnsAsync(_query);
            var service = new ReportService(mock.Object, _conf);
            var reportInfo = new ReportInfo
            {
                Percent = 10,
                Query = _queryGuid,
                Result = new ReportResult
                {
                    CountSignIn = "1",
                    UserId = _userGuid
                }
            };

            //Act
            var result = await service.GetReport(_queryGuid);

            // Assert
            var reportInfoObject = JsonConvert.SerializeObject(reportInfo);
            var resultObject = JsonConvert.SerializeObject(result);
            Assert.AreEqual(reportInfoObject, resultObject);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public async Task IsValidPercentCalculation(int iterationsCount)
        {
            // Arrange
            var mock = new Mock<IReportRepository>();
            mock.Setup(p => p.Get(_queryGuid)).ReturnsAsync(_query);
            mock.Setup(p => p.GetAll()).ReturnsAsync(new[] { _query });
            var service = new ReportService(mock.Object, _conf);

            //Act
            var koef = (double)_frequency / _queryDelay * 100;
            var expectedResult = _query.Percent;
            for (var i = 0; i < iterationsCount; i++)
            {            
                await service.UpdatePercent();
                expectedResult += (byte)koef;
            }
            if (expectedResult > 100)
            {
                expectedResult = 100;
            }

            var result = await service.GetReport(_queryGuid);

            //Assert
            Assert.AreEqual(expectedResult, result.Percent);
        }
    }
}
