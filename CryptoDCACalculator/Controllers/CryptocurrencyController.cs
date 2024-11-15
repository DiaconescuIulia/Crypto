using CryptoDCACalculator.DTOs;
using CryptoDCACalculator.Entities;
using CryptoDCACalculator.Servicies.IServicies;
using Microsoft.AspNetCore.Mvc;

namespace CryptoDCACalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptocurrencyController : ControllerBase
    {
        private readonly ICryptocurrencyService _cryptoService;

        public CryptocurrencyController(ICryptocurrencyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cryptocurrency>>> GetAllCryptocurrencies()
        {
            var cryptocurrencies = await _cryptoService.GetAllCryptocurrenciesAsync();
            return Ok(cryptocurrencies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cryptocurrency>> GetCryptocurrency(Guid id)
        {
            var cryptocurrency = await _cryptoService.GetCryptocurrencyByIdAsync(id);
            if (cryptocurrency == null)
            {
                return NotFound();
            }
            return Ok(cryptocurrency);
        }

        [HttpGet("InvestInfo")]
        public async Task<ActionResult<List<CryptocurrencyInvestmentDTO>>> GetCryptocurrencyInvestitionInfo([FromQuery] List<Guid>? ids)
        {
            
            if (!ids.Any())
            {
                return BadRequest("Please provide at least one valid ID.");
            }

            var cryptocurrencyInvestments = await _cryptoService.GetCryptocurrencyInvestmentsByIdsAsync(ids);

            return Ok(cryptocurrencyInvestments);
        }

        [HttpPost("Invest")]
        public async Task<IActionResult> Invest([FromBody]List<InvestRequestDTO> investsRequest)
        {
            await _cryptoService.Invest(investsRequest);
            return Ok();
        }

    }
}
