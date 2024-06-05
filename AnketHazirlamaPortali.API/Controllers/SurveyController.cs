using AnketHazirlamaPortali.API.Dtos;
using AnketHazirlamaPortali.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace AnketHazirlamaPortali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public SurveyController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<SurveyDto> GetList()
        {
            var surveys = _context.Surveys.ToList();
            var surveysDtos = _mapper.Map<List<SurveyDto>>(surveys);
            return surveysDtos;
        }

        [HttpGet]
        [Route("{id}")]
        public SurveyDto Get(int id)
        {
            var survey = _context.Surveys.Where(s => s.Id == id).SingleOrDefault();
            var surveyDto = _mapper.Map<SurveyDto>(survey);
            return surveyDto;
        }


        [HttpGet]
        [Route("{id}/Questions")]
        public List<QuestionDto> GetQuestion(int id)
        {
            var questions = _context.Questions.Where(q => q.SurveyId == id).ToList();
            var questionDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionDtos;
        }

        [HttpPost]
        public ResultDto Post(SurveyDto dto)
        {
            if (_context.Surveys.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Anket Adı Kayıtlıdır!";
                return result;
            }
            var survey = _mapper.Map<Survey>(dto);
            survey.Updated = DateTime.Now;
            survey.Created = DateTime.Now;
            _context.Surveys.Add(survey);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Anket Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(SurveyDto dto)
        {
            var survey = _context.Surveys.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (survey == null)
            {
                result.Status = false;
                result.Message = "Anket Bulunamadı!";
                return result;
            }
            survey.Name = dto.Name;
            survey.IsActive = dto.IsActive;
            survey.Description = dto.Description;
            survey.Updated = DateTime.Now;
            survey.CategoryId = dto.CategoryId;
            _context.Surveys.Update(survey);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Anket Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var survey = _context.Surveys.Where(s => s.Id == id).SingleOrDefault();
            if (survey == null)
            {
                result.Status = false;
                result.Message = "Anket Bulunamadı!";
                return result;
            }
            _context.Surveys.Remove(survey);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Anket Silindi";
            return result;
        }


    }
}
