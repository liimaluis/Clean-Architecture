using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Service
{
    public class ProductService : IProductService
    {
         private IMediator mediator;
         private readonly IMapper mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        
        }

        //public async Task<ProductDTO> GetProductCategory(int? id)
        //{
        //    var productQuery = new GetProductByIdQuery(id.Value);

        //    if (productQuery == null)
        //    {
        //        throw new Exception($"Entity could not loaded");
        //    }

        //    var result = await mediator.Send(productQuery);

        //    return mapper.Map<ProductDTO>(result);
        //}

        public async Task<ProductDTO> GetById(int? id)
        {
            var productQuery = new GetProductByIdQuery(id.Value);

            if (productQuery == null)
            {
                throw new Exception($"Entity could not loaded");
            }

            var result = await mediator.Send(productQuery);

            return mapper.Map<ProductDTO>(result);

        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {

            var productsQuery = new GetProductsQuery();
            if (productsQuery == null)
            {
                throw new Exception($"Entity could not loaded");
            }
            var result = await mediator.Send(productsQuery);
            return mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Add(ProductDTO productDTO)
        {
            var productCreateCommand = mapper.Map<ProductCreateCommand>(productDTO);
            await mediator.Send(productCreateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if (productRemoveCommand == null)
            {
                throw new Exception($"Entity could not loaded");
            }

            await mediator.Send(productRemoveCommand);

        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = mapper.Map<ProductUpdateCommand>(productDTO);
            await mediator.Send(productUpdateCommand);
        }
    }
}
