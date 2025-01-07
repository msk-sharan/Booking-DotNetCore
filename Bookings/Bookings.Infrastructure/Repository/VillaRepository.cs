using System.Linq.Expressions;
using Bookings.Application.Common.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Data;
using Bookings.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

//Althought the interface is on the Application project the implementations should be on teh Infrastructure Project

//The implementation of teh repository is added with specified class and the Ivilla repository is implemented here
public class VillaRepository : Repository<Villa>,IVillaRepository
{
    private readonly ApplicationDbContext _db;

    // Weare implementing the db as base because in teh repository there is a db so we should say to use this db
    public VillaRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    // public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
    // {
    //     //This line gets teh villa table from the database and made it quryable using entity framework
    //     IQueryable<Villa> query = _db.Set<Villa>();
    //     if (filter != null)
    //     {
    //         query = query.Where(filter);
    //     }
    //
    //     if (!string.IsNullOrEmpty(includeProperties))
    //     {
    //         foreach (var includeProp in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
    //         {
    //             query = query.Include(includeProp);
    //         }
    //     }
    //
    //     return query.ToList();
    // }
    //
    // public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
    // {
    //     IQueryable<Villa> query = _db.Set<Villa>();
    //     if (filter != null)
    //     {
    //         query = query.Where(filter);
    //     }
    //
    //     if (!string.IsNullOrEmpty(includeProperties))
    //     {
    //         foreach (var includeProp in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
    //         {
    //             query = query.Include(includeProp);
    //         }
    //     }
    //
    //     return query.FirstOrDefault();
    // }
    //
    // public void Add(Villa entity)
    // {
    //     _db.Add(entity);
    // }
    //
    // public void Remove(Villa entity)
    // {
    //     _db.Remove(entity);
    // }

    public void Update(Villa entity)
    {
        _db.Update(entity);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}

