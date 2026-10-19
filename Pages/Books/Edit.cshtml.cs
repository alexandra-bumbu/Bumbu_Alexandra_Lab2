
using Bumbu_Alexandra_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bumbu_Alexandra_Lab2.Pages.Books;

public class EditModel : BookCategoriesPageModel
{
    private readonly Bumbu_Alexandra_Lab2Context _context;

    public EditModel(Bumbu_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (book is null)
        {
            return NotFound();
        }

        Book = book;

        PopulateAssignedCategoryData(_context, Book);

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

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        if (id is null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bookToUpdate is null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.AuthorName,
            b => b.Price,
            b => b.PublishingDate,
            b => b.AuthorID,
            b => b.PublisherID))
        {
            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        Book = bookToUpdate;

        PopulateAssignedCategoryData(_context, Book);

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
}
