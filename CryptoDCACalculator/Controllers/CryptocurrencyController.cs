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

        //[HttpGet("InvestInfo/{id}")]
        //public async Task<ActionResult<List<CryptocurrencyInvestmentDTO>>> GetCryptocurrencyInvestitionInfo(Guid id)
        //{
        //    List<CryptocurrencyInvestmentDTO> cryptocurrencyInvestments = new List<CryptocurrencyInvestmentDTO>();
        //    var investmentInfo = await _cryptoService.GetCryptocurrencyInvestmentsByIdAsync(id);
        //    if (investmentInfo == null)
        //    {
        //        return NotFound();
        //    }
        //    cryptocurrencyInvestments.Add(investmentInfo);
        //    return Ok(cryptocurrencyInvestments);
        //}

        [HttpGet("InvestInfo")]
        public async Task<ActionResult<List<CryptocurrencyInvestmentDTO>>> GetCryptocurrencyInvestitionInfo([FromQuery] List<Guid>? ids)
        {
            
            if (!ids.Any())
            {
                return BadRequest("Please provide at least one valid ID.");
            }

            var cryptocurrencyInvestments = await _cryptoService.GetCryptocurrencyInvestmentsByIdsAsync(ids);

            if (cryptocurrencyInvestments == null || !cryptocurrencyInvestments.Any())
            {
                return NotFound("No investments found for the provided IDs.");
            }

            return Ok(cryptocurrencyInvestments);
        }

    }
}
