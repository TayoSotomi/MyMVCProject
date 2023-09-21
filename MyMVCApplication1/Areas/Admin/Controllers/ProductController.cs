using Microsoft.AspNetCore.Mvc;
using SampleProject1.DataAccess.Repository.IRepository;
using SampleProject1.Models;

namespace SampleProject1Web.Areas.Admin.Controllers
{   
   
        [Area("Admin")]
        public class ProductController : Controller
        {
            
            private readonly IUnitOfWork _unitOfWork;
            public ProductController(IUnitOfWork unitOfWork)
            {
              
                _unitOfWork = unitOfWork;
            }
            public IActionResult Index()
            {
                List<Product> productList = _unitOfWork.Product.GetAll().ToList();

                return View(productList);
            }

            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Create(Product obj)
            {
                if (obj.Title == obj.Author.ToString())
                {
                    ModelState.AddModelError("title", "Title and Author cant have the same value");
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.Product.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product was created succesfully";
                    return RedirectToAction("Index");
                }
                return View();

            }

            public IActionResult Edit(int id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                return View(productFromDb);
            }

            [HttpPost]
            public IActionResult Edit(Product obj)
            {

                if (ModelState.IsValid)
                {
                    _unitOfWork.Product.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product was updated succesfully";
                    return RedirectToAction("Index");
                }
                return View();

            }
            public IActionResult Delete(int id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                return View(productFromDb);
            }
            [HttpPost, ActionName("Delete")]
            public IActionResult DeletePost(int? id)
            {
                Product obj = _unitOfWork.Product.Get(u => u.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.Product.Remove(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product was deleted succesfully";
                    return RedirectToAction("Index");
                }
                return View();

            }
        }
    
}
