namespace LibraryManagement.Models
{
    public class BookLocation
    {
        public string Room { get; }
        public string Shelf { get; }

        public BookLocation(string room, string shelf)
        {
            Room = room;
            Shelf = shelf;
        }

        public override string ToString()
        {
            return $"Room: {Room}, Shelf: {Shelf}";
        }
    }
}
