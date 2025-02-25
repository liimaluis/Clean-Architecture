using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products Not Found");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetByIdProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var products = await productService.GetById(id);

            if (products == null)
            {
                return NotFound("Product Not Found");
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Invalid Data");
            }

            await productService.Add(product);

            return new CreatedAtRouteResult("GetByIdProduct", new { id = product.Id }, product);

            //return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest("Invalid Id");
            }

            if (product == null)
            {
                return BadRequest("invalid Data");
            }

            await productService.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await productService.GetById(id);

            if (product == null)
            {
                return NotFound("Product Not Found");
            }

            await productService.Remove(id);
            return Ok(product);
        }
    }
}
