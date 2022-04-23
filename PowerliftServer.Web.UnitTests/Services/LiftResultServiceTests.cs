using Microsoft.Extensions.Logging;
using Moq;
using PowerliftServer.Web.Models;
using PowerliftServer.Web.Services;
using Xunit;

namespace PowerliftServer.Web.UnitTests.Services
{
    public class LiftResultServiceTests
    {
        private Mock<ILogger<LiftResultService>> mockLogger;
        private LiftResultService service;
        private bool eventRaised;
        private int numberOfEventCalls;
        private LiftResult liftResultPassedToEvent;

        public LiftResultServiceTests()
        {
            this.eventRaised = false;
            this.numberOfEventCalls = 0;
            this.mockLogger = new Mock<ILogger<LiftResultService>>();
            this.service = new LiftResultService(mockLogger.Object);
            this.service.LiftResultUpdated += MockEventHandler;
        }

        [Fact]
        public void UpdateLiftResultData_ReturnsWithoutFiringEvent_IfLiftAttemptNull_Test()
        {
            this.service.UpdateLiftResultData(null);
            Assert.False(this.eventRaised);
        }

        [Fact]
        public void UpdateLiftResultData_ReturnsWithoutFiringEvent_IfEventHandlerNull_Test()
        {
            this.service = new LiftResultService(mockLogger.Object);
            this.service.UpdateLiftResultData(new LiftResult());
        }

        [Fact]
        public void UpdateLiftResultData_TriggersEvent_WhenNonNullLifterRecieved_Test()
        {
            var liftResult = new LiftResult { LiftSuccessful = true };
            this.service.UpdateLiftResultData(liftResult);
            Assert.True(this.eventRaised);
            Assert.Equal(liftResult.LiftSuccessful, this.liftResultPassedToEvent.LiftSuccessful);
        }

        [Fact]
        public void LiftAttemptUpdated_CanOnlybeSubscribedToOnce()
        {
            var liftResult = new LiftResult();

            // Subscribe to the event multiple times. Overlaoded adder should only let a single subscription occur.
            this.service.LiftResultUpdated += MockEventHandler;
            this.service.LiftResultUpdated += MockEventHandler;
            this.service.LiftResultUpdated += MockEventHandler;
            this.service.LiftResultUpdated += MockEventHandler;
            this.service.LiftResultUpdated += MockEventHandler;

            // Verify this by asserting that event has only been triggered a single time
            this.service.UpdateLiftResultData(liftResult);

            Assert.Equal(1, this.numberOfEventCalls);
        }

        private void MockEventHandler(object sender, LiftResult liftAttempt)
        {
            this.eventRaised = true;
            this.numberOfEventCalls++;
            this.liftResultPassedToEvent = liftAttempt;
        }
    }
}
