using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IDatabaseAdapter
    {
        Task<bool> CreateWhiskeyReview(WhiskeyReview whiskeyReview);

        Task<List<WhiskeyReview>> GetWhiskeyReviews(string userId);

        Task<WhiskeyReview> GetWhiskeyReview(string userId, string id);

        Task<bool> UpdateWhiskeyReview(string userId, string id, WhiskeyReview whiskeyReview);

        Task<bool> DeleteWhiskeyReview(string userId, string id);
    }
}