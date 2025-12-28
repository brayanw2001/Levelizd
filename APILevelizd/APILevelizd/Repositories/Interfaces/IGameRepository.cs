using APILevelizd.Models;
using Microsoft.EntityFrameworkCore;

namespace APILevelizd.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Review> GameReviews(int gameId);
    }
}
