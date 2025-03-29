using Dapper;
using API.Domain.DTOs;
using API.Domain.IRepositories;
using API.Shared.Helpers;

namespace API.Infrastructure.Repositories
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly DbConnectionHelper _conHelper;
        private readonly QueryHelper _queryHelper;

        public InventoryRepo(DbConnectionHelper conHelper, QueryHelper queryHelper)
        {
            _conHelper = conHelper;
            _queryHelper = queryHelper;
        }

        public async Task<Book> AddBookWithCopies(Book newBook)
        {
            try
            {
                var query = _queryHelper.GetQuery("AddBookWithCopies");

                var parameters = new
                {
                    p_title = newBook.Title,
                    p_author = newBook.Author,
                    p_genre = newBook.Genre,
                    p_language = newBook.Language,
                    p_isbn = newBook.ISBN,
                    p_created_by = newBook.CreatedBy,
                    p_number_of_copies = newBook.NumberOfCopies
                };

                using (var connection = _conHelper.CreateConnection())
                {
                    //var bookId = await connection.ExecuteScalarAsync<int>(query, parameters);
                    var bookId = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);


                    newBook.BookId = bookId;

                    return newBook;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}