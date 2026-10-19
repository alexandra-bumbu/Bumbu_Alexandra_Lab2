using Microsoft.AspNetCore.Mvc;
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

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}
