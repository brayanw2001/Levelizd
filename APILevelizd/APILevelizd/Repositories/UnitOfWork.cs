    using APILevelizd.Context;
using APILevelizd.Repositories.Interfaces;

namespace APILevelizd.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGameRepository? _gameRepository;
        
        private IReviewRepository? _reviewRepository;
        
        private IUserRepository? _userRepository;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository
        {
            get
            {
                return _gameRepository = _gameRepository ?? new GameRepository(_context);
            }
        }

        public IReviewRepository ReviewRepository
        {
            get
            {
                return _reviewRepository = _reviewRepository ?? new ReviewRepository(_context);
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
