using System.IO;
using System.Text.Json;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class FileBookRepository : IBookRepository
    {
        public void Save(Book book)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
            Directory.CreateDirectory(folder);

            var filename = Path.Combine(folder, $"{book.Title} - {book.Author}.json");

            var json = JsonSerializer.Serialize(book);
            File.WriteAllText(filename, json);
        }
    }
}
