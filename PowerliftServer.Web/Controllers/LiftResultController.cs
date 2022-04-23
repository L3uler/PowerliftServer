using Microsoft.AspNetCore.Mvc;
using PowerliftServer.Web.Models;
using PowerliftServer.Web.Services;

namespace PowerliftServer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiftResultController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<LiftResultController> logger;

        /// <summary>
        /// Service for processing lift results received into the controller.
        /// </summary>
        private readonly ILiftResultService liftResultService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftResultController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="liftResultService">Service for processing lift results received into the controller.</param>
        public LiftResultController(ILogger<LiftResultController> logger, ILiftResultService liftResultService)
        {
            this.logger = logger;
            this.liftResultService = liftResultService;
        }

        /// <summary>
        /// Accepts a POST request containing a LiftResult object to be processed.
        /// </summary>
        /// <param name="liftResult">The lift result to be processed.</param>
        /// <returns>200 if lift result data successfully processed. 500 otherwise.</returns>
        public IActionResult PostLiftResultAsync(LiftResult liftResult)
        {
            this.logger.LogInformation($"Received POST request from {Request.HttpContext.Connection.RemoteIpAddress}");

            try
            {
                this.liftResultService.UpdateLiftResultData(liftResult);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error updating overlay data", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(liftResult);
        }
    }
}
