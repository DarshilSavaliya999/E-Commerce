using Bulky_razor.Data;
using Bulky_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> categoriesList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;  
        }
        public void OnGet()
        {
            categoriesList = _db.Categories.ToList();
        }
    }
}
