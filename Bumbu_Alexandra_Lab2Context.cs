
using Bumbu_Alexandra_Lab2.Models;
using Microsoft.EntityFrameworkCore;

public class Bumbu_Alexandra_Lab2Context(DbContextOptions<Bumbu_Alexandra_Lab2Context> options) : DbContext(options)
{
    public DbSet<Bumbu_Alexandra_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Bumbu_Alexandra_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Bumbu_Alexandra_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Category> Category { get; set; } = default!;
}
