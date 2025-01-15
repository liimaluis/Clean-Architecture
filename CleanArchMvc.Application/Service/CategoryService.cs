using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            this.categoryRepository = categoryRepository;
                
            this.mapper = mapper;
        }

        public Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> GetById()
        {
            throw new NotImplementedException();
        }

        public Task Add(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task Update(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
