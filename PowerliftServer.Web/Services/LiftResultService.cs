using PowerliftServer.Web.Models;

namespace PowerliftServer.Web.Services
{
    /// <summary>
    /// Service for processing <see cref="LiftResult"/> data that is received through the API.
    /// </summary>
    public class LiftResultService : ILiftResultService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<LiftResultService> logger;

        /// <summary>
        /// Backing field for the LiftAttemptUpdated property.
        /// </summary>
        private event EventHandler<LiftResult> liftResultUpdated;

        /// <summary>
        /// Event to be triggered when new <see cref="LiftResult"/> data is received.
        /// </summary>
        public event EventHandler<LiftResult> LiftResultUpdated 
        {
            add
            {
                if (this.liftResultUpdated == null) this.liftResultUpdated += value;
            }
            remove
            {
                this.liftResultUpdated -= value;
            }
        }

        public LiftResultService(ILogger<LiftResultService> logger)
        {
            this.logger = logger;
        }

        public void UpdateLiftResultData(LiftResult liftResult)
        {
            if (liftResult == null) return;
            if (this.liftResultUpdated == null)
            {
                this.logger.LogError("Cannot trigger LiftResultUpdated event since nothing is subscribed");
                return;
            }
            this.logger.LogInformation("Triggering LiftResultUpdated event.");
            this.liftResultUpdated(this, liftResult);
        }
    }
}
