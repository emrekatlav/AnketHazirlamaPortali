
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.FileProviders;
using NuGet.Protocol;
using AnketHazırlamaPortalı;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnketHazırlamaPortalı.Controllers
{

    
    public class AdminController : Controller
    {
        

        private readonly IConfiguration _configuration;
        

        public AdminController(IConfiguration configuration)
        {

            
            _configuration = configuration;
            
           
        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("Users")]
        public IActionResult AdminUser()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

        [Route("AdminCategories")]
        public IActionResult AdminCategories()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

        public IActionResult AdminRole()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }

		public IActionResult AdminSurvey()
		{
			string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
			ViewBag.ApiBaseUrl = ApiBaseUrl;
			return View();
		}
        public IActionResult AdminQuestion()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }
        public async Task< IActionResult> GetRoleList()
        {
            
            return View();
        }

        public IActionResult RoleAdd()
        {
            return View();
        }


        












    }
}
