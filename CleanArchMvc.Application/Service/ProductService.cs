using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Service
{
    public class ProductService : IProductService
    {
         private IProductRepository productRepository;
         private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        
        }

        public async Task<IEnumerable<ProductDTO>> GetProductCategory(int? id)
        {
            var productEntity = await productRepository.GetProductCategoryAsync(id);
            return mapper.Map<IEnumerable<ProductDTO>>(productEntity);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productEntity = await productRepository.GetByIdAsync(id);
            return mapper.Map<ProductDTO>(productEntity);

        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productEntity = await productRepository.GetProducstAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(productEntity);
        }

        public async Task Add(ProductDTO productDTO)
        {
            var producEntity = mapper.Map<Product>(productDTO);
            await productRepository.CreateAsync(producEntity);
        }

        public async Task Remove(int? id)
        {
            var producEntity = productRepository.GetByIdAsync(id).Result;
            await productRepository.RemoveAsync(producEntity);

        }

        public async Task Update(ProductDTO productDTO)
        {
            var productEntity = mapper.Map<Product>(productDTO);
            await productRepository.UpdateAsync(productEntity);
        }
    }
}
