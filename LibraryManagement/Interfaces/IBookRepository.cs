using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IBookRepository
    {
        void Save(Book book);
    }
}
