using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Api.TaxaJuros.Model;

namespace Api.TaxaJuros.Controllers
{
   [ApiController]
   [Route("taxaJuros")]
   public class TaxaJurosController : ControllerBase
   {
       private readonly ILogger<TaxaJurosController> _logger;
 
       public TaxaJurosController(ILogger<TaxaJurosController> logger)
       {
           _logger = logger;
       }
 
       [HttpGet]
       public double Get()
       {
           _logger.LogInformation("TaxaJuros: "+ Juro.Valor);
           return Juro.Valor;
       }
   }
}
