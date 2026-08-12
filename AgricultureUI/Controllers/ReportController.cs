using AgricultureUI.Models;
using ClosedXML.Excel;
using DataLayer.Contexts;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AgricultureUI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StaticReport()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Dosya1");

                workSheet.Cell(1, 1).Value = "Ürün Adı";
                workSheet.Cell(1, 2).Value = "Ürün Kategorisi";
                workSheet.Cell(1, 3).Value = "Ürün Stok";

                workSheet.Cell(2, 1).Value = "Mercimek";
                workSheet.Cell(2, 2).Value = "Bakliyat";
                workSheet.Cell(2, 3).Value = "785 Kg";

                workSheet.Cell(3, 1).Value = "Buğday";
                workSheet.Cell(3, 2).Value = "Bakliyat";
                workSheet.Cell(3, 3).Value = "1.986 Kg";

                workSheet.Cell(4, 1).Value = "Havuç";
                workSheet.Cell(4, 2).Value = "Sebze";
                workSheet.Cell(4, 3).Value = "167 Kg";

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BakliyatRaporu.xlsx");
                }
            }
        }

        public List<ContactModel> ContactList()
        {
            List<ContactModel> contactModels = new List<ContactModel>();
            using (var context = new AgricultureContext())
            {
                contactModels = context.Contacts.Select(x => new ContactModel
                {
                    ContactId = x.ContactID,
                    ContactName = x.Name,
                    ContactDate = x.Date,
                    ContactMail = x.Mail,
                    ContactMessage = x.Message
                }).ToList();
            }
            return contactModels;
        }
        public IActionResult ContactReport()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Mesaj Listesi");
                workSheet.Cell(1, 1).Value = "Mesaj ID";
                workSheet.Cell(1, 2).Value = "Mesaj Gönderen";
                workSheet.Cell(1, 3).Value = "Mail Adresi";
                workSheet.Cell(1, 4).Value = "Mesaj İçeriği";
                workSheet.Cell(1, 5).Value = "Mesaj Tarihi";

                int contactRowCount = 2;
                foreach (var item in ContactList())
                {
                    workSheet.Cell(contactRowCount, 1).Value = item.ContactId;
                    workSheet.Cell(contactRowCount, 2).Value = item.ContactName;
                    workSheet.Cell(contactRowCount, 3).Value = item.ContactMail;
                    workSheet.Cell(contactRowCount, 4).Value = item.ContactMessage;
                    workSheet.Cell(contactRowCount, 5).Value = item.ContactDate;
                    contactRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MesajRapor.xlsx");
                }
            }
        }
        public List<AnnouncementModel> AnnouncementList()
        {
            List<AnnouncementModel> announcementModels = new List<AnnouncementModel>();
            using (var context = new AgricultureContext())
            {
                announcementModels = context.Announcements.Select(x => new AnnouncementModel
                {
                  Id = x.AnnouncementId,
                  Title = x.Title,
                  Description = x.Description,
                  Date = x.Date,
                  Status = x.Status
                }).ToList();
            }
            return announcementModels;
        }
        public IActionResult AnnouncementReport()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Duyuru Listesi");
                workSheet.Cell(1, 1).Value = "Duyuru ID";
                workSheet.Cell(1, 2).Value = "Duyuru Başlığı";
                workSheet.Cell(1, 3).Value = "Duyuru Açıklaması";
                workSheet.Cell(1, 4).Value = "Duyuru Tarihi";
                workSheet.Cell(1, 5).Value = "Duyuru Durumu";

                int contactRowCount = 2;
                foreach (var item in AnnouncementList())
                {
                    workSheet.Cell(contactRowCount, 1).Value = item.Id;
                    workSheet.Cell(contactRowCount, 2).Value = item.Title;
                    workSheet.Cell(contactRowCount, 3).Value = item.Description;
                    workSheet.Cell(contactRowCount, 4).Value = item.Date;
                    workSheet.Cell(contactRowCount, 5).Value = item.Status;
                    contactRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DuyuruRapor.xlsx");
                }
            }
        }
    }

}
