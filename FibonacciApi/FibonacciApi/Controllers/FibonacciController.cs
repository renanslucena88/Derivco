using FibonacciApi.Core.Domain.Interfaces;
using FibonacciApi.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FibonacciApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _service;

        public FibonacciController( IFibonacciService service)
        {
            _service = service;
        }

        // GET: api/<FibonacciController>
        [HttpGet]
        public async Task<IActionResult> GetFibonacci([FromBody] FibonacciRequest request)
        {
            try
            {
                _service.ValidadeRequest(request);

                var result = await _service.Fibonacci(request).ConfigureAwait(false);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult EndPointTest()
        {
            return Ok("I'm alive");
        }
    }
}