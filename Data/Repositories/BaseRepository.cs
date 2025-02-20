
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    #region Transaction Management

    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();

    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

    }

    #endregion

    public virtual void Add(TEntity entity)
    {
        try
        {
             _dbSet.AddAsync(entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while creating {nameof(TEntity)} entity : {ex.Message}");
          
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {

        try
        {
         
            IQueryable<TEntity> query = _dbSet;

            if (includeExpression != null)
                query = includeExpression(query);

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while loading {nameof(TEntity)} : {ex.Message}");
            return null!;

        }
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {

        try
        {

            IQueryable<TEntity> query = _dbSet;

            if (includeExpression != null)
                query = includeExpression(query);

            return await query.FirstOrDefaultAsync(predicate);
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while loading {nameof(TEntity)} : {ex.Message}");
            return null!;

        }

    }

    public virtual void Update(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while updating {nameof(TEntity)} entity : {ex.Message}");
           
        }
    }

    public virtual void Delete(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while deleting {nameof(TEntity)} entity : {ex.Message}");
          
        }

    }

    public virtual async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

}
