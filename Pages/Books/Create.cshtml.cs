
using Bumbu_Alexandra_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bumbu_Alexandra_Lab2.Pages.Books;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Bumbu_Alexandra_Lab2Context _context;

    public CreateModel(Bumbu_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = new Book();

    public IActionResult OnGet()
    {
        Book = new Book
        {
            BookCategories = new List<BookCategory>()
        };

        PopulateAssignedCategoryData(_context, Book);

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName"
        );

        ViewData["AuthorID"] = new SelectList(
            _context.Author,
            "ID",
            "LastName"
        );

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        string[] selectedCategories)
    {
        if (!ModelState.IsValid)
        {
            Book.BookCategories ??= new List<BookCategory>();

            PopulateAssignedCategoryData(_context, Book);

            foreach (var category in AssignedCategoryDataList)
            {
                category.Assigned = selectedCategories
                    .Contains(category.CategoryID.ToString());
            }

            ViewData["PublisherID"] = new SelectList(
                _context.Publisher,
                "ID",
                "PublisherName",
                Book.PublisherID
            );

            ViewData["AuthorID"] = new SelectList(
                _context.Author,
                "ID",
                "LastName",
                Book.AuthorID
            );

            return Page();
        }

        Book.BookCategories = new List<BookCategory>();

        foreach (var categoryID in selectedCategories)
        {
            if (int.TryParse(categoryID, out int id))
            {
                Book.BookCategories.Add(new BookCategory
                {
                    CategoryID = id
                });
            }
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
