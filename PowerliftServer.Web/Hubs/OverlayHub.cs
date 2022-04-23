using Microsoft.AspNetCore.SignalR;
using PowerliftServer.Web.Models;

namespace PowerliftServer.Web.Hubs
{
    /// <summary>
    /// SingalR hub for transmitting <see cref="LiftAttempt"/> data.
    /// </summary>
    public class OverlayHub : Hub
    {
        /// <summary>
        /// Sends a SignalR update to clients containing new lift attempt data.
        /// </summary>
        /// <param name="liftAttempt">The lift attempt to be sent to clients.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task UpdateAttempt(LiftAttempt liftAttempt)
        {
            await Clients.All.SendAsync("UpdateAttempt", liftAttempt);
        }

        /// <summary>
        /// Sends a SignalR update to clients containing new lift result data.
        /// </summary>
        /// <param name="liftAttempt">The lift result to be sent to clients.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task UpdateResult(LiftResult liftResult)
        {
            await Clients.All.SendAsync("UpdateResult", liftResult);
        }
    }
}
