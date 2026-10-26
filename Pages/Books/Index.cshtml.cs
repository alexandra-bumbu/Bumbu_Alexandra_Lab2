
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

    public string TitleSort { get; set; } = "";

    public string AuthorSort { get; set; } = "";

    public string CurrentFilter { get; set; } = "";

    public async Task OnGetAsync(
        int? id,
        int? categoryID,
        string? sortOrder,
        string? searchString)
    {
        BookD = new BookData();

        TitleSort = string.IsNullOrEmpty(sortOrder)
            ? "title_desc"
            : "";

        AuthorSort = sortOrder == "author"
            ? "author_desc"
            : "author";

        CurrentFilter = searchString ?? "";

        // Incarcam cartile
        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        // Cautam dupa titlu sau autor
        if (!string.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books
                .Where(b =>
                    b.Title.Contains(searchString,
                        StringComparison.OrdinalIgnoreCase)
                    || (b.Author != null &&
                        (b.Author.FirstName.Contains(searchString,
                            StringComparison.OrdinalIgnoreCase)
                        || b.Author.LastName.Contains(searchString,
                            StringComparison.OrdinalIgnoreCase))))
                .ToList();
        }

        // Selectarea cartii
        if (id != null)
        {
            BookID = id.Value;

            Book? book = BookD.Books
                .SingleOrDefault(b => b.ID == id.Value);

            if (book != null)
            {
                BookD.Categories = book.BookCategories!
                    .Select(bc => bc.Category!)
                    .ToList();
            }
        }

        // Categoria selectata
        if (categoryID != null)
        {
            CategoryID = categoryID.Value;
        }

        // Sortarea cartilor
        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Title)
                    .ToList();
                break;

            case "author_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Author?.FullName)
                    .ToList();
                break;

            case "author":
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Author?.FullName)
                    .ToList();
                break;

            default:
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Title)
                    .ToList();
                break;
        }
    }
}
