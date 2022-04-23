using PowerliftServer.Web.Models;

namespace PowerliftServer.Web.Services
{
    public interface ILiftAttemptService
    {
        public event EventHandler<LiftAttempt> LiftAttemptUpdated;
        public void UpdateLiftAttemptData(LiftAttempt liftAttempt);
    }
}
