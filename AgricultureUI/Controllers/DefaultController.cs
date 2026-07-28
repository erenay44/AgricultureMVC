using BussinessLayer.Concrete;
using DataLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureUI.Controllers
{
    public class DefaultController : Controller
    {
        ServiceManager serviceManager = new ServiceManager(new EfServiceDal());
        public IActionResult Index()
        {
            var values = serviceManager.GetListAll();
            return View(values);
        }
    }
}
