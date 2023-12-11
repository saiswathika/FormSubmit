using FormSubmit.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FormSubmit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserForm()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public IActionResult UserForm(UserViewModel uvm)
        {
            uvm.Id = Guid.NewGuid();
            string jsonObject = JsonConvert.SerializeObject(uvm);
            string filePath = _configuration.GetValue<string>("FilePath");

            if (System.IO.File.Exists(filePath))
                System.IO.File.WriteAllText(filePath, jsonObject);
            else
            {
                using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (var writer = new StreamWriter(file, Encoding.UTF8))
                    {
                        writer.Close();
                        file.Close();
                        System.IO.File.WriteAllText(filePath, jsonObject);
                    }
                }
                }
               
            ViewBag.SuccessMessage = " Record has been submitted successfully!";
            ModelState.Clear();

            return View(new UserViewModel());
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