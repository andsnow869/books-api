namespace Api.Exceptions;

//исключение, если книга не найдена по id
public class BookNotFoundException : Exception
{
    public BookNotFoundException(Guid id)
       : base($"Книга с id: {id} не существует")
    {
    }
}
