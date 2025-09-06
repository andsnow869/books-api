using Carter;
using Mapster;

namespace Api.Books.UpdateBook;

public record UpdateBookRequest(
    Guid Id,
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
);

//результат выполнения команды
public record UpdateBookResponse(bool IsSuccess);


//конечная точка API, этот класс отвечает за обновление книги
//реализует интерфейс ICartetModule (это часть библиотеки Carter, упрощает регистрацию endpointов в ASP.NET)
public class UpdateBookEndpoint : ICarterModule
{
    //регистрируем маршрут
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //MapPut("/books") - значит, что этот endpoint сработает, когда придет HTTP PUT-запрос на /books
        app.MapPut("/books", async (

            //UpdateBookRequest request - данные, которые приходят от клиента(id книги, заголовок, описание и т.д)
            UpdateBookRequest request,

            //ISender sender - это интерфейс MediatR(паттерн CQRS)
            //вместо того, чтобы писать бизнес-логику прямо в контроллере, мы отправляем команду UpdateBookCommand через sender
            //она уходит в отдельный Handler, где реально выполняется обновление в базе 
            ISender sender
        ) =>
        {
            //request - это данные, которые пришли от клиента (например, JSOn из API)
            //Adapt<UpdateBookCommand>() - говорит Mapster: сделай мне объект типа UpdateBookCommand и скопируй туда данные из request
            //перевод из API-слоя в бизнес-логику
            var command = request.Adapt<UpdateBookCommand>();

            //отправляем команду в MediatR, он вызовет нужный обработчик
            var result = await sender.Send(command);

            //result - это результат выполнения команды (true или false в моем случае)
            //result.Adapt<UpdateBookResponse>() - говорит Mapster: сделай объект UpdateBookResponse для клиента, скопировав нужные данные из result
            //перевод из бизнес-логики обратно в ответ API
            var response = result.Adapt<UpdateBookResponse>();

            //возвращает 200 OK c JSON-ответом {"isSuccess": true/false}
            return Results.Ok(response);
        });
    }
}
