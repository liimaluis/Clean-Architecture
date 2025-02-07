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
        public ProductsController(IProductService productService, ICategoryService categoryService) 
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProducts();
            return View(products);
        }

        // Retorna o formulário para a Criação da Categoria
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        // Ação que realiza a Criação da Categoria
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

        // Retorna o formulário para a Edição da Categoria
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

            return View(products);
        }

        // Ação que realiza a Edição da Categoria
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
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
            return View(product);
        }

        // Retorna o formulário para a Edição da Categoria
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

            return View(products);
        }
    }
}
