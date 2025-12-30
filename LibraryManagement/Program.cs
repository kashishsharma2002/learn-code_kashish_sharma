using LibraryManagement.Models;
using LibraryManagement.Services;

var location = new BookLocation("Reading Room", "Shelf 5");

var book = new Book("A Great Book", "John Doe", location);

var printer = new HtmlPrinter();
var repo = new FileBookRepository();

printer.PrintPage(book.GetCurrentPage());
repo.Save(book);

Console.WriteLine($"Book Location: {book.Location}");
