using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Api.CalculoJuros.Controllers
{
    [ApiController]
    [Route("calculajuros")]
    public class CalculoJurosController : ControllerBase
    {
        private readonly ILogger<CalculoJurosController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public CalculoJurosController(ILogger<CalculoJurosController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<string> Get(decimal valorInicial, int meses)
        {
            _logger.LogInformation("CalculoJuros");

            var taxaJuros = await GetTaxaJuros();
            var valorFinal = await CalculaValorFinal(valorInicial, meses, taxaJuros);

            _logger.LogInformation("ValorFinal CalculaJuros: " + valorFinal.ToString("#.##"));

            return valorFinal.ToString("#.##");
        }

        private async Task<double> GetTaxaJuros()
        {
            try
            {
                _logger.LogInformation("TaxaJuros client call");

                var request = new HttpRequestMessage(HttpMethod.Get, "/taxaJuros");
                var client = _clientFactory.CreateClient("TaxaJuros");
                var response = await client.SendAsync(request);

                _logger.LogInformation("Response sucess status: " + response.IsSuccessStatusCode);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<double>(responseStream);
                }
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogInformation("Exception: " + e);
                throw new Exception();
            }
        }

        private async Task<decimal> CalculaValorFinal(decimal valorInicial, int meses, double taxaJuros)
        {
            _logger.LogInformation("CalculaValorFinal");
            decimal valorComTaxaDeJuros = valorInicial;
            double valorDoJuros = 0;

            while (meses > 0)
            {
                valorDoJuros = Convert.ToDouble(valorComTaxaDeJuros) * taxaJuros;
                valorComTaxaDeJuros = valorComTaxaDeJuros + Convert.ToDecimal(valorDoJuros);
                meses--;
            }

            return valorComTaxaDeJuros;
        }
    }
}
