namespace CrudConsole
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }

        public Book(string title, string author, int publishYear)
        {
            Title = title;
            Author = author;
            PublishYear = publishYear;
        }

        public override string ToString()
        {
            return $"{Title},{Author},{PublishYear}";
        }
    }
}
