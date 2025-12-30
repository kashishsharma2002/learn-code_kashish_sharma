using System.IO;
using System.Text.Json;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class FileBookRepository : IBookRepository
    {
        private string GetFolderPath()
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
            Directory.CreateDirectory(folder); 
            return folder;
        }

        private void WriteBookToFile(string path, Book book)
        {
            var json = JsonSerializer.Serialize(book);
            File.WriteAllText(path, json); 
        }

        public void Save(Book book)
        {
            try
            {
                var folder = GetFolderPath();
                var filename = Path.Combine(folder, $"{book.Title} - {book.Author}.json");

                WriteBookToFile(filename, book);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
