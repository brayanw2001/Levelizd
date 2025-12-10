using APILevelizd.Models;

namespace APILevelizd.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<Review> UserReviews(string userName);
    }
}
