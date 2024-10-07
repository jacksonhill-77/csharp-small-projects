namespace CrudConsole
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }

        public Book() { }
        public Book(int id, string title, string author, int publishYear)
        {
            ID = id;
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
