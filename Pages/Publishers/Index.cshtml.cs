
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bumbu_Alexandra_Lab2.Models;
using Bumbu_Alexandra_Lab2.ViewModels;

namespace Bumbu_Alexandra_Lab2.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly Bumbu_Alexandra_Lab2Context _context;

    public IndexModel(Bumbu_Alexandra_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public PublisherIndexData PublisherData { get; set; } = new();

    public int PublisherID { get; set; }

    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();

        PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books)
            .ThenInclude(c => c.Author)
            .OrderBy(i => i.PublisherName)
            .ToListAsync();

        Publisher = PublisherData.Publishers.ToList();

        if (id != null)
        {
            PublisherID = id.Value;

            Publisher? publisher = PublisherData.Publishers
                .SingleOrDefault(i => i.ID == id.Value);

            if (publisher != null)
            {
                PublisherData.Books = publisher.Books
                    ?? new List<Book>();
            }
        }
    }
}
