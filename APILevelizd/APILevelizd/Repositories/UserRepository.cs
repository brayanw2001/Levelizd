using APILevelizd.Context;
using APILevelizd.Models;
using APILevelizd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APILevelizd.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Review> UserReviews(string userName)
        {
                       // va na tabela de usuarios
                                            // alem dos usuarios, inclua suas reviews
                                                                            // filtre pelos usuarios que tenham o nome buscado
            var user = _context.Users.Include(u => u.Reviews).FirstOrDefault(u => u.Name == userName);

            if (user is null)
                return null;

            return user.Reviews.ToList();
        }
    }
}

