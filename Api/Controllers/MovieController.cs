using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly Service.Movie _srvMovie;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
            _srvMovie = new Service.Movie();
        }

        [HttpGet]
        [Route("upcoming/{page?}")]
        public async Task<IActionResult> Get(int? page)
        {
            try
            {
                page = page ?? 1;
                var retorno = await _srvMovie.ProximosLancamentos(page.Value);

                return Ok(retorno);

            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(e.Message);
            }
        }
    }
}
