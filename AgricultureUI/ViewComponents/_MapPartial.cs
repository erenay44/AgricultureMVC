using DataLayer.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureUI.ViewComponents
{
    public class _MapPartial:ViewComponent
    {
       public IViewComponentResult Invoke()
        {
            AgricultureContext context = new AgricultureContext();
            var values = context.Addresses.Select(x => x.MapInfo).FirstOrDefault();
            ViewBag.v = values;
            return View();
        }
    }
}
