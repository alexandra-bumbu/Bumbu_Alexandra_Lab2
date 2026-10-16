
using Bumbu_Alexandra_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bumbu_Alexandra_Lab2.Pages.Books;

public class CreateModel : PageModel
{
    private readonly Bumbu_Alexandra_Lab2Context _context;

    public CreateModel(Bumbu_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Publisher, "ID", "PublisherName");

        ViewData["AuthorID"] = new SelectList(
            _context.Author, "ID", "LastName");

        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(
                _context.Publisher, "ID", "PublisherName");

            ViewData["AuthorID"] = new SelectList(
                _context.Author, "ID", "LastName");

            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
