namespace CrudConsole
{
    public class Book
    {
        public int PID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }

        public Book(int pid, string title, string author, int publishYear)
        {
            PID = pid;
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
