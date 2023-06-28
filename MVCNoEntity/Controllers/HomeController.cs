using Microsoft.AspNetCore.Mvc;
using MVCNoEntity.Data;
using MVCNoEntity.Models;
using System.Diagnostics;

namespace MVCNoEntity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Produto> produtos = Database.getAllProducts();
            return View(produtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Produto product)
        {
            Database.AddProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Produto product = Database.GetSingleProdut(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Produto(int id)
        {
            Produto product = Database.GetSingleProdut(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Excluir(int id)
        {
            Database.Excluir(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}