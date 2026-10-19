
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bumbu_Alexandra_Lab2.Models;

namespace Bumbu_Alexandra_Lab2.Pages.Books;

public class IndexModel : PageModel
{
    private readonly Bumbu_Alexandra_Lab2Context _context;

    public IndexModel(Bumbu_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    public BookData BookD { get; set; } = new BookData();

    public int BookID { get; set; }

    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (id != null)
        {
            BookID = id.Value;

            Book? book = BookD.Books
                .Where(b => b.ID == id.Value)
                .SingleOrDefault();

            if (book != null)
            {
                BookD.Categories = book.BookCategories!
                    .Select(bc => bc.Category!)
                    .ToList();
            }
        }

        if (categoryID != null)
        {
            CategoryID = categoryID.Value;
        }
    }
}
