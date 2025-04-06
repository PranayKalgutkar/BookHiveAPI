using Dapper;
using API.Domain.DTOs;
using API.Domain.IRepositories;
using API.Shared.Helpers;

namespace API.Infrastructure.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly DbConnectionHelper _conHelper;
        private readonly QueryHelper _queryHelper;

        public CartRepo(DbConnectionHelper conHelper, QueryHelper queryHelper)
        {
            _conHelper = conHelper;
            _queryHelper = queryHelper;
        }
        public async Task<string> AddBookToCart(AddToCart addToCart)
        {
            try
            {
                var query = _queryHelper.GetQuery("AddBookToCart");

                var parameters = new
                {
                    p_user_id = addToCart.UserId,
                    p_book_id = addToCart.BookId
                };

                using (var connection = _conHelper.CreateConnection())
                {
                    var result = await connection.QuerySingleAsync<string>(query, parameters);

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> SchedulePickup(SchedulePickup request)
        {
            try
            {
                var query = _queryHelper.GetQuery("SchedulePickup");

                var parameters = new
                {
                    p_user_id = request.UserId,
                    p_scheduled_on = request.ScheduledOn
                };

                using (var connection = _conHelper.CreateConnection())
                {
                    var result = await connection.QuerySingleAsync<string>(query, parameters);
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