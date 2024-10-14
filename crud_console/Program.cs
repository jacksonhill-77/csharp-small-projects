using Newtonsoft.Json;

namespace CrudConsole
{
    class Program
    {
        public static string filePath = "C:\\Users\\Jackson\\source\\repos\\practice\\crud_console\\WrittenLines.json";
        static void Main()
        {

            bool run = true;

            Console.WriteLine("Connecting to database...");
            EntityMethods.QueryBooks();

            Console.WriteLine("Welcome to the Simple Library.");
            Interface.PrintOptions();

            do
            {
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        DisplayBooks();
                        break;
                    case "2":
                        AddBooks();
                        break;
                    case "3":
                        RemoveBook();
                        break;
                    case "4":
                        UpdateBook();
                        break;
                    case "5":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Please choose a number between 1 - 4");
                        break;

                }
                Interface.PrintOptions();
            } while (run == true);
        }

        // These 4 methods become part of the database class

        static void DisplayBooks()
        {
            Console.WriteLine("Current books in library:");
            Interface.PrintLines(FileInteracter.ReadLinesFromFile(filePath), filePath);
        }

        static void AddBooks()
        {
            List<String> books = FileInteracter.ReadLinesFromFile(filePath).ToList();
            List<String> newBooks = ConvertBookListToJSON(GetUsersListOfBooks());

            books.AddRange(newBooks);

            FileInteracter.WriteLinesToFile(books, filePath);
        }

        static void RemoveBook()
        {
            int chosenBook = GetIndexOfBookToModify("remove");
            List<string> lines = FileInteracter.ReadLinesFromFile(filePath).ToList();
            lines.RemoveAt(chosenBook);
            FileInteracter.WriteLinesToFile(lines, filePath);
            Console.WriteLine("Book removed.");
            DisplayBooks();
        }

        static void UpdateBook()
        {
            int bookIndex = GetIndexOfBookToModify("update");
            List<string> lines = FileInteracter.ReadLinesFromFile(filePath).ToList();
            string book = lines[bookIndex];
            string updatedBook = ChangeBookProperties(book);
            lines[bookIndex] = updatedBook;
            FileInteracter.WriteLinesToFile(lines, filePath);
            
        }
}
