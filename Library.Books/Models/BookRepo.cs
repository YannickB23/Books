using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Library.Books.Models
{
    public class BookRepo
    {
        public void AddBook(Book book)
        {
            string sql = "INSERT INTO Book(Title, Author, Price, Description, CountryID)" +
                         "VALUES(@Title, @Author, @Price, @Description, @CountryID)";

            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Query(sql, book);
            }
        }

        public int AddBookReturnId(Book book)
        {
            string sql = "INSERT INTO Book(Title, Author, Price, Description, CountryID)" +
                         "VALUES(@Title, @Author, @Price, @Description, @CountryID)" +
                         "SELECT CAST(SCOPE_IDENTITY() as int)";
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var returnId = connection.Query<int>(sql, book).SingleOrDefault();
                return returnId;
            }
        }

        public List<Book> GetAllBooks()
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                return connection.Query<Book>("SELECT * FROM Book").ToList();
            }
        }

        public void UpdateBook(Book book)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
                connection.Execute("UPDATE Book SET Title = @title, Author = @author, Price = @price, " +
                                   "Description = @description, CountryID = @countryid WHERE ID = @Id",
                    new
                    {
                        id = book.Id,
                        title = book.Title,
                        author = book.Author,
                        price = book.Price,
                        description = book.Description,
                        countryid = book.CountryId
                    });
        }

        public void DeleteBookId(int id)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute("DELETE FROM Book WHERE ID = @Id", new {Id = id});
            }
        }

        public List<Country> GetCountries()
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                return connection.Query<Country>("SELECT * FROM Country").ToList();
            }
        }

        public List<Book> GetAuthorsFromSelectedCountry(int id)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var list = connection.Query<Book>("SELECT Author FROM Book WHERE CountryId =@id",
                    new {Id = id}).ToList();
                return list;
            }
        }

    }
}
