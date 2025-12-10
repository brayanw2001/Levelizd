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
            
        public IEnumerable<Review> GameReviews(string name) // retorna todos os comentarios do jogo especificado
        {
            var game = _context.Games.Include(g => g.Reviews).FirstOrDefault(g => g.Name == name);

            return game.Reviews.ToList();
        }
    }
}
