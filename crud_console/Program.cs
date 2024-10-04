namespace CrudConsole
{
    class Program
    {
        static string filePath = "C:\\Users\\Jackson\\source\\repos\\practice\\crud_console\\WrittenLines.csv";
        static void Main()
        {
            bool run = true;
            Interface.PrintWelcomeScreen();

            while (run == true)
            {
                Interface.PrintOptions();
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Interface.PrintLines(FileInteracter.ReadLinesFromFile(filePath), filePath);
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
            }
        }

        static void AddBooks()
        {
            string filePath = "C:\\Users\\Jackson\\source\\repos\\practice\\crud_console\\WrittenLines.csv";
            List<String> csvBooks = ConvertBookListToCSVStrings(GetUsersListOfBooks());
            FileInteracter.WriteLinesToFile(csvBooks, filePath);
        }

        static void RemoveBook()
        {
            int chosenBook = GetIndexOfBookToModify("remove");
            List<string> lines = FileInteracter.ReadLinesFromFile(filePath).ToList();
            lines.RemoveAt(chosenBook);
            FileInteracter.WriteLinesToFile(lines, filePath);
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
        static string ChangeBookProperties(string book)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Please select the part of the book you wish to update by selecting 1-3: ");
                Console.WriteLine(Interface.ConvertLineToPropertiesList(book));
                int chosenProperty = int.Parse(Console.ReadLine()) - 1;
                ModifyBook(book, chosenProperty);
                Console.WriteLine("\nDo you wish to continue editing? y/ n");
                string continueEditing = Console.ReadLine();
                if (continueEditing == "y")
                {
                    continue;
                }
                else if (continueEditing == "n")
                {
                    break;
                }
            }

            return book;
        }

        static string ModifyBook(string book, int propertyIndex)
        {
            string updatedBook = ChangeSinglePropertyOfBook(book, propertyIndex)
            PrintUpdatedBookProperties(updatedBook);
            return updatedBook;
        }

        static string ChangeSinglePropertyOfBook(string book, int propertyIndex)
        {
            string[] properties = book.Split(',');
            Console.WriteLine("\nPlease enter what you would like to update to: ");
            string newProperty = Console.ReadLine();
            properties[propertyIndex] = newProperty;
            string updatedBook = String.Join(",", properties);
            return updatedBook;
        }

        static string PrintUpdatedBookProperties(string updatedBook)
        {
            Console.WriteLine("\nUpdated. New properties are as follows: ");
            Console.WriteLine(Interface.ConvertLineToPropertiesList(updatedBook));
        }

        static int GetIndexOfBookToModify(string modificationType)
        {
            Console.WriteLine($"Please select the number of a book to {modificationType}:");
            Interface.PrintLines(FileInteracter.ReadLinesFromFile(filePath), filePath);
            return int.Parse(Console.ReadLine()) - 1;
        }
                   

        static List<String> ConvertBookListToCSVStrings(List<Book> books)
        {
            List<String> output = new List<String>();
            foreach (Book book in books)
            {
                output.Add(book.ToString());
            }
            return output;
        }

        static List<Book> GetUsersListOfBooks()
        {
            bool run = true;
            List<Book> books = new List<Book>();

            do
            {
                books.Add(GetUserInputAsBook());
                Console.WriteLine("\nDo you wish to add another book? Y/N");
                string userResponse = Console.ReadLine();
                if (userResponse == "y")
                {
                    continue;
                }
                else if (userResponse == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid response, please try again.");
                };

            } while (run);

            return books;
        }
        static Book GetUserInputAsBook()
        {
            Console.WriteLine("\nPlease enter the book title: ");
            string title = Console.ReadLine();

            Console.WriteLine("\nPlease enter the author name: ");
            string author = Console.ReadLine();

            Console.WriteLine("\nPlease enter the publish date: ");
            int publishDate = int.Parse(Console.ReadLine());

            return new Book(title, author, publishDate);
        }
    }

    class Interface
    {
        static public void PrintWelcomeScreen()
        {
            Console.WriteLine("\nWelcome to the Simple Library. Please enter a number from the options below:\n");
            PrintOptions();
        }

        static public void PrintOptions()
        {
            Console.WriteLine("1. Display current books in the library");
            Console.WriteLine("2. Add a book");
            Console.WriteLine("3. Remove a book");
            Console.WriteLine("4. Update a book's contents");
            Console.WriteLine("5. Close application");
        }
        static public void PrintLines(string[] lines, string filePath)
        {
            Console.WriteLine("\n");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(ConvertLineToReadableInfo(lines[i], i));
            };
        }

        static public string ConvertLineToReadableInfo(string line, int index)
        {
            string[] properties = line.Split(',');
            return $"{index + 1}. Title: {properties[0]}. Author: {properties[1]}. Year published: {int.Parse(properties[2])}";
        }

        static public string ConvertLineToPropertiesList(string line)
        {
            string[] properties = line.Split(',');
            return $"1. Title: {properties[0]}\n2. Author: {properties[1]}\n3. Year published: {int.Parse(properties[2])}";
        }
    }

    class FileInteracter
    {
        static public void WriteLinesToFile(List<String> lines, string filePath)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        static public string[] ReadLinesFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }

    }

    class Book
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
