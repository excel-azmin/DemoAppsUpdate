using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string ApiKey = "http://localhost:5224/api";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       
        public async Task<IActionResult> Index()
        {


            var data = await GetAllEmployee();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EmployeeViewModel model)
        {
            var employee = await CreateEmployee(model);
            var data = await GetAllEmployee();
            return View(data);
        }



        public async Task<List<EmployeeViewModel>> GetAllEmployee()
        {
            using var clinet = new HttpClient();
            var responce = await clinet.GetAsync($"{ApiKey}/Employees");
            string result = await responce.Content.ReadAsStringAsync();
            var emp = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);
            return emp ?? new List<EmployeeViewModel>();
        }


        public async Task<EmployeeViewModel> CreateEmployee(EmployeeViewModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            using var clinet = new HttpClient();
            var httpcontent = new StringContent(data, Encoding.UTF8, "application/json");
            var responce = await clinet.PostAsync($"{ApiKey}/Employees", httpcontent);
            string result = await responce.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeViewModel>(result);
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}