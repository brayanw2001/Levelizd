using APILevelizd.Context;
using APILevelizd.Models;
using APILevelizd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APILevelizd.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext context) : base(context)
        {    
        }
            
        public IEnumerable<Review> GameReviews(int gameId) 
        {
            return _context.Reviews.Where(r => r.GameId == gameId).ToList();
        }
    }
}
