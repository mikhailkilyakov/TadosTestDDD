namespace Domain.Services
{
    using Entities;

    public interface IEntityService<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);

        TEntity Get(int id);

        void Delete(int id);
    }
}