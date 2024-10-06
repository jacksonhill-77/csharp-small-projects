using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CrudConsole
{
    public class DatabaseConnect
    {
        public static String connectionString = "Server=localhost;User ID=sa;Password=9n8kZ81J0iuB;Initial Catalog=SIMPLE_LIBRARY;Integrated Security=false;TrustServerCertificate=True";
        
        static public void ConnectToDB()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM dbo.simple_library WHERE Author = @authorName";

                var books = connection.Query<Book>(sql, new { authorName = "F. Scott Fitzgerald" }).ToList();

                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
        }
        
    }
}
