namespace Domain.Repositories
{
    using Entities;
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(int id);

        void Add(TEntity entity);

        void Save(TEntity entity);

        void Delete(TEntity entity);
    }
}