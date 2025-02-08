
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return null!;

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while creating {nameof(TEntity)} entity : {ex.Message}");
            return null!;
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

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        if (updatedEntity == null)
            return null!;

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;

            if (existingEntity == null)
                return null!;


            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return existingEntity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while updating {nameof(TEntity)} entity : {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;

            if (existingEntity == null)
                return false;

            _dbSet.Remove(existingEntity);
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error occurred while deleting {nameof(TEntity)} entity : {ex.Message}");
            return false;
        }

    }

    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }

}
