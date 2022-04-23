using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PowerliftServer.Web.Hubs;
using PowerliftServer.Web.Models;
using PowerliftServer.Web.Services;

namespace PowerliftServer.Web.Pages
{
    /// <summary>
    /// Backing model for the Index.cshtml file.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<IndexModel> logger;

        /// <summary>
        /// Service from which lift attempt updated events will be sent.
        /// </summary>
        private readonly ILiftAttemptService liftAttemptService;

        /// <summary>
        /// Service from which lift result updated events will be sent.
        /// </summary>
        private readonly ILiftResultService liftResultService;

        /// <summary>
        /// SingalR HubContext for pushing lift data out to clients in real time.
        /// </summary>
        private readonly IHubContext<OverlayHub> overlayHubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexModel"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="liftAttemptService"></param>
        /// <param name="overlayHubContext"></param>
        public IndexModel(
            ILogger<IndexModel> logger,
            ILiftAttemptService liftAttemptService,
            ILiftResultService liftResultService,
            IHubContext<OverlayHub> overlayHubContext)
        {
            this.logger = logger;
            this.liftAttemptService = liftAttemptService;
            this.liftResultService = liftResultService;
            this.overlayHubContext = overlayHubContext;
            this.liftAttemptService.LiftAttemptUpdated += OnLiftAttemptUpdated;
            this.liftResultService.LiftResultUpdated += OnLiftResultUpdated;
        }

        /// <summary>
        /// Sends lift attempt data out to clients using the SignalR hub. Used in subscribing to the service's LiftAttemptUpdated event.
        /// </summary>
        /// <param name="sender">Object from which the event was sent.</param>
        /// <param name="liftAttempt">The lift attempt data to be sent to clients by SignalR.</param>
        public void OnLiftAttemptUpdated(object sender, LiftAttempt liftAttempt)
        {
            this.overlayHubContext.Clients.All.SendAsync("UpdateAttempt", liftAttempt); // Send new lift attempt data to client
            this.logger.LogInformation("Updated lift attempt overlay data.");
        }

        /// <summary>
        /// Sends lift result data out to clients using the SignalR hub. Used in subscribing to the service's LiftResultUpdated event.
        /// </summary>
        /// <param name="sender">Object from which the event was sent.</param>
        /// <param name="liftAttempt">The lift result data to be sent to clients by SignalR.</param>
        public void OnLiftResultUpdated(object sender, LiftResult liftResult)
        {
            this.overlayHubContext.Clients.All.SendAsync("UpdateResult", liftResult);
            this.logger.LogInformation("Updated lift result overlay data.");
        }
    }
}