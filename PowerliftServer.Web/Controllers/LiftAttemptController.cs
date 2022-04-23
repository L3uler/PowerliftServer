using Microsoft.AspNetCore.Mvc;
using PowerliftServer.Web.Models;
using PowerliftServer.Web.Services;

namespace PowerliftServer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiftAttemptController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<LiftAttemptController> logger;

        /// <summary>
        /// Service for processing lift attempts received into the controller.
        /// </summary>
        private readonly ILiftAttemptService liftAttemptService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftAttemptController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="liftAttemptService">Service for processing lift attempts received into the controller.</param>
        public LiftAttemptController(ILogger<LiftAttemptController> logger, ILiftAttemptService liftAttemptService)
        {
            this.logger = logger;
            this.liftAttemptService = liftAttemptService;
        }

        /// <summary>
        /// Accepts a POST request containing a LiftAttempt object to be processed.
        /// </summary>
        /// <param name="liftAttempt">The lift attempt to be processed.</param>
        /// <returns>200 if lifter attempt data successfully processed. 500 otherwise.</returns>
        [HttpPost]
        public IActionResult PostLiftAttemptAsync(LiftAttempt liftAttempt)
        {
            this.logger.LogInformation($"Received POST request from {Request.HttpContext.Connection.RemoteIpAddress}");

            try
            {
                this.liftAttemptService.UpdateLiftAttemptData(liftAttempt);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error updating overlay data", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(liftAttempt);
        }

        /// <summary>
        /// Provides a means for clients to test service life by sending a GET request.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TestServiceLife()
        {
            this.logger.LogInformation($"Received GET request from {Request.HttpContext.Connection.RemoteIpAddress}");
            return Ok($"Service running.");
        }
    }
}