using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CrudConsole
{
    public class DapperDBConnect
    {
        public static String connectionString = "Server=localhost;User ID=sa;Password=9n8kZ81J0iuB;Initial Catalog=SIMPLE_LIBRARY;Integrated Security=false;TrustServerCertificate=True";
        
        static public void ConnectToDB()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM dbo.simple_library";

                var books = connection.Query<Book>(sql).ToList();

                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
        }
        
    }

    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public static String connectionString = "Server=localhost;User ID=sa;Password=9n8kZ81J0iuB;Initial Catalog=SIMPLE_LIBRARY;Integrated Security=false;TrustServerCertificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
    public class EntityMethods
    {

        public static void QueryBooks()
        {
            using (var context = new LibraryContext())
            {
                Console.WriteLine("Testing...");
                var books = context.Books.ToList();
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }
    }
}
