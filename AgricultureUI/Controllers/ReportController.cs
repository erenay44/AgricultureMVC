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
            ExcelPackage excelPackage = new ExcelPackage();
            var workBook = excelPackage.Workbook.Worksheets.Add("Dosya1");

            workBook.Cells[1, 1].Value = "Ürün Adı";
            workBook.Cells[1, 2].Value = "Ürün Kategorisi";
            workBook.Cells[1, 3].Value = "Ürün Stok";

            workBook.Cells[2, 1].Value = "Mercimek";
            workBook.Cells[2, 2].Value = "Bakliyat";
            workBook.Cells[2, 3].Value = "785 Kg";

            workBook.Cells[3, 1].Value = "Buğday";
            workBook.Cells[3, 2].Value = "Bakliyat";
            workBook.Cells[3, 3].Value = "1.986 Kg";

            workBook.Cells[4, 1].Value = "Havuç";
            workBook.Cells[4, 2].Value = "Sebze";
            workBook.Cells[4, 3].Value = "167 Kg";

            var bytes = excelPackage.GetAsByteArray();
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BakliyatRaporu.xlsx");
        }


    }

}
