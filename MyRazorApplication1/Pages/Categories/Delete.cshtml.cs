using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApplication1.Data;
using MyRazorApplication1.Models;

namespace MyRazorApplication1.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
               
            private readonly ApplicationDbContext _db;
            public Category CategoryDelete { get; set; }
            public DeleteModel(ApplicationDbContext db)
            {
                _db = db;
            }
            public void OnGet(int? id)
            { 
                if (id != null && id != 0)
                {
                    CategoryDelete = _db.Categories.Find(id);
                }
            }

            public IActionResult OnPost()
            {
            Category? obj = _db.Categories.Find(CategoryDelete.Id);
            if (obj == null)
            {
                return NotFound();
            }           
               _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category was deleted succesfully";
                return RedirectToPage("Index");
                      
           
            }
        
    }
}
