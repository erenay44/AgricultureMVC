using BussinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureUI.Controllers
{
    public class TestController : Controller
    {
        private readonly IServiceService _serviceService;

        public TestController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            var values = _serviceService.GetListAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddService() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddService(Service service) 
        {
            _serviceService.Insert(service);    
            return RedirectToAction("Index");
        }
    }
}
