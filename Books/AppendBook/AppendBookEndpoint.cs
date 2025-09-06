namespace Api.Books.AppendBook;

//AppendBookRequest — это данные, которые приходят от клиента, когда он хочет добавить книгу (заголовок, автор, описание, цена и т.д.).
public record AppendBookRequest(
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
);

//AppendBookResponse — это ответ, который сервер вернёт обратно клиенту (в нашем случае только Id новой книги).
public record AppendBookResponse(Guid Id);

//класс, который отвечает за добавление книги
public class AppendBookEndpoint : ICarterModule
//: ICarterModule – это значит, что твой класс реализует интерфейс ICarterModule.
//У интерфейса ICarterModule есть один метод, который ты обязана реализовать:
//void AddRoutes(IEndpointRouteBuilder app);
//по итогу: AppendBookEndpoint : ICarterModule = "Это модуль Carter, который умеет добавлять свой маршрут /books в приложение".
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/books", async (AppendBookRequest request, ISender sender) =>
        {
            // app.MapPost("/books", ...) — мы регистрируем новый POST-запрос по адресу /books.
            //(AppendBookRequest request, ISender sender) — это параметры хендлера:
            //request — данные, которые клиент прислал в теле запроса.
            //sender — посредник (из MediatR), чтобы отправить команду в нужный Handler.
            var command = request.Adapt<AppendBookCommand>();
            //Сравнивает поля Title, Name, Description, ImageUrl, Price, Category.
            //Находит совпадения по имени.
            //Переносит значения.
            var result = await sender.Send(command);
            //sender — посредник (из MediatR), чтобы отправить команду в нужный Handler.
            //Handler сохраняет книгу в базе и возвращает AppendBookResult.
            var response = result.Adapt<AppendBookResponse>();
            //Результат из Handler (внутренняя модель) мы снова переводим в формат ответа для клиента (AppendBookResponse).
            return Results.Ok(response);
            //Возвращаем 200 OK и JSON с Id книги
        });
    }
}
