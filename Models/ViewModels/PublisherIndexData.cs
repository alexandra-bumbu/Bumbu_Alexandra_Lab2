
using System.Collections.Generic;
using Bumbu_Alexandra_Lab2.Models;

namespace Bumbu_Alexandra_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();

        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
