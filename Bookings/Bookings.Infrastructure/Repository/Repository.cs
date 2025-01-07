using System.Linq.Expressions;
using Bookings.Application.Common.Interfaces;
using Bookings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Repository;

//THis is teh generic repository that implements teh IRepository
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    //We are using dbset to simplify the code and make it easy fir understanding
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        //we are making dbset as generic 
        dbSet = _db.Set<T>();
    }
    
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return query.FirstOrDefault();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public bool Any(Expression<Func<T, bool>> filter)
    {
        return dbSet.Any(filter);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
}