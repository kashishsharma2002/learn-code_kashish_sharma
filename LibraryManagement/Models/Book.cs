namespace LibraryManagement.Models
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public int CurrentPage { get; private set; } = 1;

        public BookLocation Location { get; private set; }

        public Book(string title, string author, BookLocation location)
        {
            Title = title;
            Author = author;
            Location = location;
        }

        public void TurnPage()
        {
            CurrentPage++;
        }

        public string GetCurrentPage()
        {
            return $"Content of page {CurrentPage}";
        }
    }
}
