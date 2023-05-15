using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client;

        public ProductController()
        {
            client = new() { BaseAddress = new Uri("http://localhost:5072") };
        }

        public async Task<IActionResult> Index()
        {
            var res = await client.GetAsync("api/product");
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Product>>(content));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var res = await client.GetAsync($"api/product/{id}");
            if (res.StatusCode == HttpStatusCode.NotFound) return NotFound();
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);
            if (product == null) return NotFound();
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var res = await client.GetAsync($"api/product/category");
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(content) ?? new List<Category>();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            ModelState.Remove(nameof(product.Category));
            if (ModelState.IsValid)
            {
                var body = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"api/product", body);
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                else return StatusCode(500);
            }
            var res = await client.GetAsync($"api/product/category");
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(content) ?? new List<Category>();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var res = await client.GetAsync($"api/product/{id}");
            if (res.StatusCode == HttpStatusCode.NotFound) return NotFound();
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);
            if (product == null) return NotFound();

            res = await client.GetAsync($"api/product/category");
            content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(content) ?? new List<Category>();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            if (id != product.ProductId) return NotFound();

            ModelState.Remove(nameof(product.Category));
            if (ModelState.IsValid)
            {
                var body = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/product/{product.ProductId}", body);
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                else return StatusCode(500);
            }

            var res = await client.GetAsync($"api/product/category");
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(content) ?? new List<Category>();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var res = await client.GetAsync($"api/product/{id}");
            if (res.StatusCode == HttpStatusCode.NotFound) return NotFound();
            var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await client.DeleteAsync($"api/product/{id}");
            if (res.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            else return StatusCode((int)res.StatusCode);
        }
    }
}
