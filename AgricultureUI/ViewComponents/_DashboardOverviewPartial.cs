using DataLayer.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureUI.ViewComponents
{
    public class _DashboardOverviewPartial:ViewComponent
    {
        AgricultureContext context = new AgricultureContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.employeeCount = context.Employees.Count();
            ViewBag.serviceCount = context.Services.Count();
            ViewBag.messageCount = context.Contacts.Count();
            ViewBag.currentMessage = 3;

            ViewBag.announcementTrue = context.Announcements.Where(x => x.Status == true).Count();
            ViewBag.announcementFalse = context.Announcements.Where(x => x.Status == false).Count();

            ViewBag.uretimSorumlusu = context.Employees.Where(x=>x.Title== "Üretim Sorumlusu").Select(y=>y.Name).FirstOrDefault();
            ViewBag.hasatPaketleme = context.Employees.Where(x=>x.Title== "Hasat ve Paketleme Şefi").Select(y=>y.Name).FirstOrDefault();
            ViewBag.ihracatSorumlusu = context.Employees.Where(x=>x.Title== "İhracat Sorumlusu").Select(y=>y.Name).FirstOrDefault();
            ViewBag.bolgeSatis = context.Employees.Where(x=>x.Title== "Bölge Satış Müdürü").Select(y=>y.Name).FirstOrDefault();
            return View();
        }
    }
}
