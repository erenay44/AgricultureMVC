using Microsoft.AspNetCore.Mvc;

namespace AgricultureUI.ViewComponents
{
    public class _DashboardOverviewPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
