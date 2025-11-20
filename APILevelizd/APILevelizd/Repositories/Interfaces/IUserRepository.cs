using APILevelizd.Models;

namespace APILevelizd.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> UserReviews();
    }
}
