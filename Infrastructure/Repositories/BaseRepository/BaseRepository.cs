using System.Data;
using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Relations.Infrastructure;
using Relations.Infrastructure.Repositories.BaseRepository;

public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
{  
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Entity> _dbSet;
    private readonly IDbConnection _dbConnection;

    public BaseRepository(ApplicationDbContext context, IDbConnection dbConnection)
    {
        _context = context;
        _dbSet = _context.Set<Entity>();
        _dbConnection = dbConnection;
    }

    public async Task<IQueryable<Entity>> GetAll()
    {
        return await Task.FromResult(_dbSet
            .AsQueryable()
            .AsNoTracking());
    }

    public async Task<Entity?> GetById(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IQueryable<Entity>> GetAllDapper()
    {
        var query = $"SELECT * FROM {typeof(Entity).Name}s";
        var result = await _dbConnection.QueryAsync<Entity>(query);

        return result.AsQueryable().AsNoTracking();
    }

    public async Task<Entity?> GetByIdDapper(long id)
    {
        var query = $"SELECT * FROM {typeof(Entity).Name}s WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstOrDefaultAsync<Entity>(query, new { Id = id });

        return result;
    }

    public async Task<Entity> Create(Entity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Entity> Update(Expression<Func<Entity, bool>> expression, Entity entity)
    {
        var existingEntity = await _dbSet
            .FirstOrDefaultAsync(expression);

        _context.Entry(existingEntity)
            .CurrentValues
            .SetValues(entity);

        await _context.SaveChangesAsync();

        return existingEntity;
    }

    public async Task<bool> Delete(Expression<Func<Entity, bool>> expression)
    {
        var entity = await _dbSet
            .FirstOrDefaultAsync(expression);

        _dbSet.Remove(entity!);
        await _context.SaveChangesAsync();

        return true;
    }
}