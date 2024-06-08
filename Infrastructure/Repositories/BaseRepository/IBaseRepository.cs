using System.Linq.Expressions;

namespace Relations.Infrastructure.Repositories.BaseRepository
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task<IQueryable<Entity>> GetAll();
        Task<Entity?> GetById(long id);
        Task<IQueryable<Entity>> GetAllDapper();
        Task<Entity?> GetByIdDapper(long id);
        Task<Entity> Create(Entity entity);
        Task<Entity> Update(Expression<Func<Entity, bool>> expression, Entity entity);
        Task<bool> Delete(Expression<Func<Entity, bool>> expression);
    }
}
