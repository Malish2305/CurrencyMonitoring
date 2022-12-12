using Microsoft.AspNetCore.Mvc;
using Computershit.Core.Models;
using Computershit.Core.Service;

namespace Computershit.Web.Controllers;


public class HomeController : Controller
{

    ProductDataProvider _productDataProvider = new ProductDataProvider();
    PurchaseDataProvider _purchaseDataProvider = new PurchaseDataProvider();
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productDataProvider.GetAll();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Buy(int id)
    {
        ViewBag.BookId = id;
        _purchaseDataProvider.CreatePurchase(await _productDataProvider.GetProductById(ViewBag.BookId), DateTime.Now);
        return View();
    }
    [HttpGet]

    public async Task<IActionResult> ShowProduct(int productId)
    {
        var product = await _productDataProvider.GetProductById(productId);
        return View(product);
    }



}