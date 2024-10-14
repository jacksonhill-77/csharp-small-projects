using System;

namespace CrudConsole
{
    public class UI
    {
        // These following methods become part of the interface/ UI class, and are used in the database access classes

        static string ChangeBookProperties(string book)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("\nPlease select the part of the book you wish to update by selecting 1-3: ");
                Console.WriteLine(Interface.ConvertLineToPropertiesList(book));
                int chosenProperty = int.Parse(Console.ReadLine()) - 1;
                book = ModifyBook(book, chosenProperty);
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
            string updatedBook = ChangeSinglePropertyOfBook(book, propertyIndex);
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

        static void PrintUpdatedBookProperties(string updatedBook)
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


        static List<String> ConvertBookListToJSON(List<Book> books)
        {
            List<String> output = new List<String>();
            foreach (Book book in books)
            {
                output.Add(JsonConvert.SerializeObject(book));
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
            // dummy pid
            int pid = 0;

            Console.WriteLine("\nPlease enter the book title: ");
            string title = Console.ReadLine();

            Console.WriteLine("\nPlease enter the author name: ");
            string author = Console.ReadLine();

            Console.WriteLine("\nPlease enter the publish date: ");
            int publishDate = int.Parse(Console.ReadLine());

            return new Book(pid, title, author, publishDate);
        }
    }
}
}

