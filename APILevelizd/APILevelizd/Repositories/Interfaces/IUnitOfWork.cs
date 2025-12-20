namespace APILevelizd.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IGameRepository GameRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public IUserRepository UserRepository { get; }

        void Commit();
    }
}
