using AnketHazirlamaPortali.API.Dtos;
using AnketHazirlamaPortali.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AnketHazirlamaPortali.API.Controllers
{
    [Route("api/UserSurveys")]
    [ApiController]
    [Authorize]
    public class UserSurveyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public UserSurveyController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        public List<SurveyDto> GetList()
        {
            string userId = GetUserId();
            var surveys = _context.Surveys.Where(s => s.CreatorUserId == userId).ToList();
            var surveysDtos = _mapper.Map<List<SurveyDto>>(surveys);
            return surveysDtos;
        }

        [HttpGet]
        [Route("{id}")]
        public SurveyDto Get(int id)
        {
            string userId = GetUserId();
            var survey = _context.Surveys.Where(s => s.Id == id && s.CreatorUserId == userId).SingleOrDefault();
            var surveyDto = _mapper.Map<SurveyDto>(survey);
            return surveyDto;
        }

        [HttpGet]
        [Route("{id}/Questions")]
        public List<QuestionDto> GetQuestion(int id)
        {
            string userId = GetUserId();
            var questions = _context.Questions.Where(q => q.SurveyId == id && q.Survey.CreatorUserId == userId).ToList();
            var questionDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionDtos;
        }

        [HttpPost]
        public ResultDto Post(SurveyDto dto)
        {
            string userId = GetUserId();
            if (_context.Surveys.Count(c => c.Name == dto.Name && c.CreatorUserId == userId) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Anket Adı Kayıtlıdır!";
                return result;
            }
            var survey = _mapper.Map<Survey>(dto);
            survey.CreatorUserId = userId;
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
            string userId = GetUserId();
            var survey = _context.Surveys.Where(s => s.Id == dto.Id && s.CreatorUserId == userId).SingleOrDefault();
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
            string userId = GetUserId();
            var survey = _context.Surveys.Where(s => s.Id == id && s.CreatorUserId == userId).SingleOrDefault();
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
