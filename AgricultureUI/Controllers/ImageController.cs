using Microsoft.AspNetCore.Mvc;
using EntityLayer.Concrete;
using BussinessLayer.Abstract;
namespace AgricultureUI.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var values = _imageService.GetListAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddImage(Image image)
        {
            _imageService.Insert(image);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteImage(int id)
        {
            var values = _imageService.GetById(id);
            _imageService.Delete(values);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditImage(int id)
        {
            var values = _imageService.GetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult EditImage(Image image) 
        {
            _imageService.Update(image);
            return RedirectToAction("Index");
        }
    }
}
