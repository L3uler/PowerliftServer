using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using PowerliftServer.Web.Hubs;
using PowerliftServer.Web.Models;
using PowerliftServer.Web.Pages;
using PowerliftServer.Web.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PowerliftServer.Web.UnitTests.Pages
{
    public class IndexTests
    {
        private Mock<ILogger<IndexModel>> mockLogger;
        private Mock<ILiftAttemptService> mockLiftAttemptService;
        private Mock<ILiftResultService> mockLiftResultService;
        private readonly Mock<IHubContext<OverlayHub>> mockHubContext;
        private readonly Mock<IHubClients> mockHubClients;
        private readonly Mock<IClientProxy> mockClientProxy;

        public IndexTests()
        {
            mockLogger = new Mock<ILogger<IndexModel>>();
            mockLiftAttemptService = new Mock<ILiftAttemptService>();
            mockLiftResultService = new Mock<ILiftResultService>();
            mockHubContext = new Mock<IHubContext<OverlayHub>>();
            mockHubClients = new Mock<IHubClients>();
            mockClientProxy = new Mock<IClientProxy>();

            mockHubContext.SetupGet(x => x.Clients).Returns(mockHubClients.Object);
            mockHubClients.SetupGet(x => x.All).Returns(mockClientProxy.Object);
        }

        [Fact]
        public void Constructor_CallsServiceEventAdders_Test()
        {
            var _ = new IndexModel(mockLogger.Object, mockLiftAttemptService.Object, mockLiftResultService.Object, mockHubContext.Object);

            mockLiftAttemptService.VerifyAdd(x => x.LiftAttemptUpdated += It.IsAny<EventHandler<LiftAttempt>>(), Times.Once);
            mockLiftResultService.VerifyAdd(x => x.LiftResultUpdated += It.IsAny<EventHandler<LiftResult>>(), Times.Once);
        }

        [Fact]
        public void OnLiftAttemptUpdated_SendsAttemptDataToClients_Test()
        {
            var liftAttempt = new LiftAttempt();
            var indexModel = new IndexModel(mockLogger.Object, mockLiftAttemptService.Object, mockLiftResultService.Object, mockHubContext.Object);

            indexModel.OnLiftAttemptUpdated(new object(), liftAttempt);

            mockHubClients.Verify(x => x.All, Times.Once);
        }

        [Fact]
        public void OnLiftResultUpdated_SendsResultDataToClients_Test()
        {
            var liftAttempt = new LiftResult();
            var indexModel = new IndexModel(mockLogger.Object, mockLiftAttemptService.Object, mockLiftResultService.Object, mockHubContext.Object);

            indexModel.OnLiftResultUpdated(new object(), liftAttempt);

            mockHubClients.Verify(x => x.All, Times.Once);
        }
    }
}
