using System.Linq.Expressions;
using Bookings.Domain.Entities;

namespace Bookings.Application.Common.Interfaces;

//This repository is for teh specific controller along with teh generic IRepository it contains additional methods
//The Ivillarepository should extend the IRepository and specify the model to it
public interface IVillaRepository : IRepository<Villa>
{
    //IEnumerable<Villa> - The method returns an enumerable collection of Villa objects. IEnumerable allows iteration over a collection of Villa instances.
    // This is typically used for read-only operations to retrieve data.
    
    //Expression<Func<Villa, bool>> - Represents a filter condition as a lambda expression (e.g., villa => villa.Price > 100).
    // The type Func<Villa, bool> means this filter is a function that takes a Villa object as input and returns a bool (true/false) based on the condition.
    // The Expression<> wrapper is used in Entity Framework or LINQ to build SQL-like queries that can be translated and executed in a database.
    
    //= null -
    // The default value is null, so if no filter is provided, all Villa records will be returned.
    
    //string? includeProperties = null - If we add a include command in teh controller we should add this and specify its datatype
    
    
    // IEnumerable<Villa> GetAll(Expression<Func<Villa,bool>>? filter=null ,String? includeProperties = null);
    // Villa Get(Expression<Func<Villa,bool>> filter ,String? includeProperties = null);
    
    //Villa entity:
    // The method expects a parameter of type Villa. This represents the entity you want to add.
    // entity:
    // This is the variable name for the Villa object being passed to the method.
    // It should be a fully constructed object with all required fields populated before calling this method.
    
    // void Add(Villa entity);
    // void Remove(Villa entity);
    void Update(Villa entity);
    void Save();

}