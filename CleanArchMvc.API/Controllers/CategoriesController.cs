using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await categoryService.GetCategories();
            if (categories == null)
            {
                return NotFound("Categories Not Found");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var categories = await categoryService.GetById(id);

            if (categories == null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO category)
        {
            if(category == null)
            {
                return BadRequest("Invalid Data");
            }

            await categoryService.Add(category);

            return new CreatedAtRouteResult("GetById", new { id = category.Id }, category);

            //return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest("Invalid Id");
            }

            if (category == null)
            {
                return BadRequest("invalid Data");
            }

            await categoryService.Update(category);

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await categoryService.GetById(id);

            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            await categoryService.Remove(id);
            return Ok(category);
        }
    }
}
