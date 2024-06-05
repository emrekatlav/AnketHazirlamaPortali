using AnketHazirlamaPortali.API.Dtos;
using AnketHazirlamaPortali.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AnketHazirlamaPortali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<CategoryDto> GetList()
        {
            var categories = _context.Categories.ToList();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public CategoryDto Get(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).SingleOrDefault();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
        [HttpGet]
        [Route("{id}/Surveys")]
        public List<SurveyDto> GetSurveys(int id)
        {
            var Surveys = _context.Surveys.Where(s => s.CategoryId == id).ToList();
            var SurveyDtos = _mapper.Map<List<SurveyDto>>(Surveys);
            return SurveyDtos;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ResultDto Post(CategoryDto dto)
        {
            if (_context.Categories.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Kategori Adı Kayıtlıdır!";
                return result;
            }
            var category = _mapper.Map<Category>(dto);
            category.Updated = DateTime.Now;
            category.Created = DateTime.Now;
            _context.Categories.Add(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kategori Eklendi";
            return result;
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ResultDto Put(CategoryDto dto)
        {
            var category = _context.Categories.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (category == null)
            {
                result.Status = false;
                result.Message = "Kategori Bulunamadı!";
                return result;
            }
            category.Name = dto.Name;
            category.IsActive = dto.IsActive;
            category.Updated = DateTime.Now;

            _context.Categories.Update(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kategori Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public ResultDto Delete(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).SingleOrDefault();
            if (category == null)
            {
                result.Status = false;
                result.Message = "Kategori Bulunamadı!";
                return result;
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kategori Silindi";
            return result;
        }
    }
}
