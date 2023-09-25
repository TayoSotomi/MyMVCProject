using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleProject1.DataAccess.Repository.IRepository;
using SampleProject1.Models;
using SampleProject1.Models.ViewModels;
using System.Collections.Generic;

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

            public IActionResult Upsert(int? id)
            {               
            
                ProductVM productVM = new()
                {
                    CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }),
                    Product = new Product()
                };
                if (id == null || id == 0)
                {
                return View(productVM);

                }
                else
                {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);

                 return View(productVM);
                }
               
            }
            [HttpPost]
            public IActionResult Upsert(ProductVM productVM,IFormFile? file)
            {              

                if (ModelState.IsValid)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product was created succesfully";
                    return RedirectToAction("Index");
                }           
                else    //ensures the dropdown is populated again        
                {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(productVM);

                }              

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
