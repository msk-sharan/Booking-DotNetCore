namespace Bookings.Application.Common.Interfaces;

//Using T is known as the generic class it specifies that Tis a class but we dont know which class
public interface IRepository<T> where T : class
{
    
}