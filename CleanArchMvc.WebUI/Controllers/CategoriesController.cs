using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetCategories();
            return View(categories);
        }

        // Retorna o formulário para a Criação da categoria
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Ação que realiza a criação da categoria
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await categoryService.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
