using APILevelizd.Context;
using APILevelizd.Models;
using APILevelizd.Repositories.Interfaces;

namespace APILevelizd.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
