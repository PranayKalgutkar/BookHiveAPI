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

        public async Task<BookCopy> AddBookWithCopies(BookCopy newBook)
        {
            try
            {
                var query = _queryHelper.GetQuery("AddBookWithCopies");

                var parameters = new
                {
                    p_title = newBook.Title,
                    p_author = newBook.Author,
                    p_genre = newBook.Genre,
                    p_book_language = newBook.Language,
                    p_isbn = newBook.ISBN,
                    p_created_by = newBook.CreatedBy,
                    p_number_of_copies = newBook.NumberOfCopies
                };

                using (var connection = _conHelper.CreateConnection())
                {
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

        public async Task<IEnumerable<BookStatus>> GetBooksByFilter(BookFilter filter)
        {
            try
            {
                var query = _queryHelper.GetQuery("GetBooksByFilter");

                var parameters = new
                {
                    p_search_text = string.IsNullOrWhiteSpace(filter.SearchText) ? null : filter.SearchText,
                    p_title = string.IsNullOrWhiteSpace(filter.Title) ? null : filter.Title,
                    p_author = string.IsNullOrWhiteSpace(filter.Author) ? null : filter.Author,
                    p_genre = string.IsNullOrWhiteSpace(filter.Genre) ? null : filter.Genre,
                    p_status = string.IsNullOrWhiteSpace(filter.Status) ? null : filter.Status,
                    p_sort_column = string.IsNullOrWhiteSpace(filter.SortColumn) ? "title" : filter.SortColumn,
                    p_sort_order = string.IsNullOrWhiteSpace(filter.SortOrder) ? "ASC" : filter.SortOrder,
                    p_page_index = filter.PageIndex,
                    p_page_size = filter.PageSize
                };

                using (var connection = _conHelper.CreateConnection())
                {
                    var result = await connection.QueryAsync<dynamic>(query, parameters);

                    var books = result.Select(row => new BookStatus
                    {
                        BookId = row.book_id,
                        Title = row.title,
                        Author = row.author,
                        Genre = row.genre,
                        Language = row.language,
                        ISBN = row.isbn,
                        CopyStatus = row.copy_status,
                        NumberOfCopies = row.status_count
                    }).ToList();

                    return books;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookStatus>> GetBooksByTitleAndStatus(string? title, string? status)
        {
            try
            {
                var query = _queryHelper.GetQuery("GetBooksByTitleAndStatus");

                var parameters = new
                {
                    p_title = title,
                    p_status = status
                };

                using (var connection = _conHelper.CreateConnection())
                {
                    var result = await connection.QueryAsync<BookStatus>(query, parameters);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}