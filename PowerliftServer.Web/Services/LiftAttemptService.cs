using PowerliftServer.Web.Models;

namespace PowerliftServer.Web.Services
{
    /// <summary>
    /// Service for processing <see cref="LiftAttempt"/> data that is received through the API.
    /// </summary>
    public class LiftAttemptService : ILiftAttemptService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<LiftAttemptService> logger;

        /// <summary>
        /// Backing field for the LiftAttemptUpdated property.
        /// </summary>
        private EventHandler<LiftAttempt> liftAttemptUpdated;

        /// <summary>
        /// Event to be triggered when new <see cref="LiftAttempt"/> data is received.
        /// </summary>
        public event EventHandler<LiftAttempt> LiftAttemptUpdated
        {
            add 
            {
                if (this.liftAttemptUpdated == null) this.liftAttemptUpdated += value;
            }
            remove
            { 
                this.liftAttemptUpdated -= value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftAttemptService"> class.
        /// </summary>
        /// <param name="logger"></param>
        public LiftAttemptService(ILogger<LiftAttemptService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Fires the LiftAttemptUpdated event to notify subscribers of new <see cref="LiftAttempt"/> data.
        /// </summary>
        /// <param name="liftAttempt">The lift attempt data to be sent with the event.</param>
        public void UpdateLiftAttemptData(LiftAttempt liftAttempt)
        {
            if (liftAttempt == null) return;
            if (this.liftAttemptUpdated == null)
            {
                this.logger.LogError("Cannot trigger LiftAttemptUpdated event since nothing is subscribed");
                return;
            }
            this.logger.LogInformation("Triggering LiftAttemptUpdated event.");
            this.liftAttemptUpdated(this, liftAttempt);
        }
    }
}
