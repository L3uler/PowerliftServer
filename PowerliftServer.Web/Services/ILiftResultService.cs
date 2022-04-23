using PowerliftServer.Web.Models;

namespace PowerliftServer.Web.Services
{
    public interface ILiftResultService
    {
        public event EventHandler<LiftResult> LiftResultUpdated;
        public void UpdateLiftResultData(LiftResult liftResult);
    }
}
