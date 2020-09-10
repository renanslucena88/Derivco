using DerivcoTest.FibonacciApi.Core.Domain.Interfaces;
using DerivcoTest.FibonacciApi.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DerivcoTest.FibonacciApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly ILogger<FibonacciController> _logger;
        private readonly ICacheFibonacciService _cacheService;
        private readonly IFibonacciService _service;

        public FibonacciController(ILogger<FibonacciController> logger, ICacheFibonacciService cacheService, IFibonacciService service)
        {
            _logger = logger;
            _cacheService = cacheService;
            _service = service;
        }

        // GET: api/<FibonacciController>
        [HttpGet]
        public async Task<IActionResult> GetFibonacci([FromBody] FibonacciRequest request)
        {
            try
            {
                var result = await _service.Fibonacci(request).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}