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
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            this.categoryRepository = categoryRepository;
                
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoryEntity = await categoryRepository.GetCategories();
            return mapper.Map<IEnumerable<CategoryDTO>>(categoryEntity);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var categoryEntity = await categoryRepository.GetById(id);
            return mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var categoryEntity = mapper.Map<Category>(categoryDTO);
            await categoryRepository.Create(categoryEntity);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var categoryEntity = mapper.Map<Category>(categoryDTO);
            await categoryRepository.Update(categoryEntity);
        }

        public async Task Remove(int? id)
        {
            var categoryEntity = categoryRepository.GetById(id).Result;
            await categoryRepository.Remove(categoryEntity);
        }
    }
}
