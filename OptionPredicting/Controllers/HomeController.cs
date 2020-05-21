using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OptionPredicting.Models;
using System.Net.Http;
using RestSharp;
using System.Text;

namespace OptionPredicting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlackSholes()
        {
            var model = new BlackSholesModel();
            return View(model);
        }

        public IActionResult MonteCarlo()
        {
            var model = new MonteCarloModel();
            return View(model);
        }

        public async Task<IActionResult> CalcBlackSholesAsync(BlackSholesModel model)
        {

            double S0 = model.S0;
            double K = model.K;
            double T = model.T;
            double sigma = model.sigma;
            double r = model.r;

            string json = $"{{\"S0\": {S0},\"K\": {K},\"T\": {T},\"sigma\": {sigma},\"r\": {r}}}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8888/api/v1/blacksholes", content);

            var responseString = await response.Content.ReadAsStringAsync();

            TempData["value"] = "Predicted Price is: " + responseString;
            return RedirectToAction("BlackSholes");
        }

        public async Task<IActionResult> CalcMonteCarloAsync(MonteCarloModel model)
        {

            double S0 = model.S0;
            double K = model.K;
            double T = model.T;
            double sigma = model.sigma;
            double r = model.r;
            double timestamp = model.timestamp;
            double samples = model.samples;

            string json = $"{{\"S0\": {S0},\"K\": {K},\"T\": {T},\"sigma\": {sigma},\"r\": {r},\"timestamp\": {timestamp},\"samples\": {samples}}}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:8888/api/v1/montecarlo", content);

            var responseString = await response.Content.ReadAsStringAsync();

            TempData["value"] = "Predicted Price is: " + responseString;
            return RedirectToAction("MonteCarlo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
