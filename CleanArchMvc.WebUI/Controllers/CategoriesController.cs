using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
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

        // Retorna o formulário para a Criação da Categoria
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Ação que realiza a Criação da Categoria
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await categoryService.Add(category);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }
            return View(category);
        }

        // Retorna o formulário para a Edição da Categoria
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var categories = await categoryService.GetById(id);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // Ação que realiza a Edição da Categoria
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await categoryService.Update(category);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }
            return View(category);
        }

        // Retorna o formulário para a Edição da Categoria
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await categoryService.GetById(id);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await categoryService.Remove(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await categoryService.GetById(id);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }
    }
}
