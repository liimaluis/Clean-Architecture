using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        private readonly ICategoryService categoryService;

        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment) 
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProducts();
            return View(products);
        }

        // Retorna o formulário para a Criação do Produto
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        // Ação que realiza a Criação da Produto
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await productService.Add(product);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }
            ViewBag.CategoryId = new SelectList(await categoryService.GetCategories(), "Id", "Name");
            return View(product);
        }

        // Retorna o formulário para a Edição do produto
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await productService.GetById(id);

            if (products == null)
            {
                return NotFound();
            }

            var categories = await categoryService.GetCategories();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", products.CategoryId);

            return View(products);
        }

        // Ação que realiza a Edição do Produto
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            //ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                try
                {
                    await productService.Update(product);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }

            ViewBag.CategoryId = new SelectList(await categoryService.GetCategories(), "Id", "Name");
            return View(product);
        }

     
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await productService.GetById(id);

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await productService.Remove(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await productService.GetById(id);

            if (products == null)
            {
                return NotFound();
            }

            var wwwroot = webHostEnvironment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + products.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(products);
        }
    }
}
