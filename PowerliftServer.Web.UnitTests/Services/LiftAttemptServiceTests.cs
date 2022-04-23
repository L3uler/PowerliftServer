using Microsoft.Extensions.Logging;
using Moq;
using PowerliftServer.Web.Models;
using PowerliftServer.Web.Services;
using Xunit;

namespace PowerliftServer.Web.UnitTests.Services
{
    public class LiftAttemptServiceTests
    {
        private Mock<ILogger<LiftAttemptService>> mockLogger;
        private LiftAttemptService service;
        private bool eventRaised;
        private int numberOfEventCalls;
        private LiftAttempt liftAttemptPassedToEvent;

        public LiftAttemptServiceTests()
        {
            this.eventRaised = false;
            this.numberOfEventCalls = 0;
            this.mockLogger = new Mock<ILogger<LiftAttemptService>>();
            this.service = new LiftAttemptService(mockLogger.Object);
            this.service.LiftAttemptUpdated += MockEventHandler;
        }

        [Fact]
        public void UpdateLiftAttemptData_ReturnsWithoutFiringEvent_IfLiftAttemptNull_Test()
        { 
            this.service.UpdateLiftAttemptData(null);
            Assert.False(this.eventRaised);
        }

        [Fact]
        public void UpdateLiftAttemptData_ReturnsWithoutFiringEvent_IfEventHandlerNull_Test()
        {
            this.service = new LiftAttemptService(mockLogger.Object);
            this.service.UpdateLiftAttemptData(new LiftAttempt());
        }

        [Fact]
        public void UpdateLiftAttemptData_TriggersEvent_WhenNonNullLifterRecieved_Test()
        {
            var liftAttempt = new LiftAttempt { CompetitionName = "Test competition name" };
            this.service.UpdateLiftAttemptData(liftAttempt);
            Assert.True(this.eventRaised);
            Assert.Equal(liftAttempt.CompetitionName, this.liftAttemptPassedToEvent.CompetitionName);
        }

        [Fact]
        public void LiftAttemptUpdated_CanOnlybeSubscribedToOnce()
        {
            var liftAttempt = new LiftAttempt();

            // Subscribe to the event multiple times. Overlaoded adder should only let a single subscription occur.
            this.service.LiftAttemptUpdated += MockEventHandler;
            this.service.LiftAttemptUpdated += MockEventHandler;
            this.service.LiftAttemptUpdated += MockEventHandler;
            this.service.LiftAttemptUpdated += MockEventHandler;
            this.service.LiftAttemptUpdated += MockEventHandler;

            // Verify this by asserting that event has only been triggered a single time
            this.service.UpdateLiftAttemptData(liftAttempt);

            Assert.Equal(1, this.numberOfEventCalls);
        }

        private void MockEventHandler(object sender, LiftAttempt liftAttempt)
        {
            this.eventRaised = true;
            this.numberOfEventCalls++;
            this.liftAttemptPassedToEvent = liftAttempt;
        }
    }
}
