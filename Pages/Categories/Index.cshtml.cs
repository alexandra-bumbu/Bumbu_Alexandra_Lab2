
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bumbu_Alexandra_Lab2.Models;

namespace Bumbu_Alexandra_Lab2.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly Bumbu_Alexandra_Lab2Context _context;

    public IndexModel(Bumbu_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = new List<Category>();

    public int CategoryID { get; set; }

    public bool IsCategorySelected { get; set; }

    public List<Book> SelectedBooks { get; set; } = new List<Book>();

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            IsCategorySelected = true;

            SelectedBooks = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .Where(b => b.BookCategories != null &&
                    b.BookCategories.Any(bc => bc.CategoryID == id.Value))
                .OrderBy(b => b.Title)
                .ToListAsync();
        }
    }
}
