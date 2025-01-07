using System.Linq.Expressions;

namespace Bookings.Application.Common.Interfaces;

//Using T is known as the generic class it specifies that Tis a class but we dont know which class
public interface IRepository<T> where T : class
{
    //We should replace all the class names to T because we don't know that whick class is going to use it
    //We are not adding update and save because update may differ for each type
    IEnumerable<T> GetAll(Expression<Func<T,bool>>? filter=null ,String? includeProperties = null);
    T Get(Expression<Func<T,bool>> filter ,String? includeProperties = null);
    void Add(T entity);
    bool Any(Expression<Func<T,bool>> filter);
    void Remove(T entity);
}