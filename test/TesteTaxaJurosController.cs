using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.TaxaJuros.Controllers;
using Api.CalculoJuros.Controllers;

namespace test
{
    [TestClass]
    public class TesteTaxaJurosController
    {
        private decimal ValorInicial { get; set; } = 100;
        private int meses { get; set; } = 5;

        [TestMethod]
        public void TestCalculaJuros()
        {
            CalculoJurosController blergh = new CalculoJurosController(null,null);
            double valorFinal = CalculoJurosController.calculajuros(ValorInicial, meses);
            Assert.AreEqual(105.10, valorFinal);
        }

        [TestMethod]
        public static void TestTaxaJurosController()
        {
            Assert.AreEqual(0.01, TaxaJurosController.Get());
        }
    }
}
