
using Carter;
namespace Api.Books.GetBooks;
using Api.Model;
using Mapster;

//Это тип данных для ответа.  
//Когда пользователь спросит `/books`, мы вернём объект `GetBooksResponse`, внутри которого список книг.
public record GetBooksResponse(IEnumerable<Book> Books);

public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    // Метод, где мы регистрируем маршруты (какой код выполняется на определённый URL).  
    //`app` — объект, который умеет добавлять маршруты. 
    {
        app.MapGet("/books", async (ISender sender) =>
        //sender.Send(...) отправляет запрос на получение книг (GetBookQuery).
        //Код пойдёт к обработчику (GetBooksQueryHandler), который достанет список книг из базы данных.
        {
            GetBooksResult result = await sender.Send(new GetBookQuery());
            GetBooksResponse response = result.Adapt<GetBooksResponse>();
            //- Мы преобразуем результат (`GetBooksResult`) в `GetBooksResponse`.  
            //- Для преобразования используется библиотека **Mapster**, чтобы не писать вручную `new GetBooksResponse(result.Books)`.  

            return Results.Ok(response);
        });
    }
}
