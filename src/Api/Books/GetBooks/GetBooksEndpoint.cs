namespace Api.Books.GetBooks;

//GetBooksRequest — это маленький объект, который хранит параметры пагинации: какая страница и сколько элементов на странице.
public record GetBooksRequest(int? PageNumber = 1, int? PageSize = 5);


//Это тип данных для ответа.  
//Когда пользователь спросит `/books`, мы вернём объект `GetBooksResponse`, внутри которого список книг.
public record GetBooksResponse(IEnumerable<Book> Books);

public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    // Метод, где мы регистрируем маршруты (какой код выполняется на определённый URL).  
    //`app` — объект, который умеет добавлять маршруты. 
    {
        app.MapGet("/books", async (
            [AsParameters] GetBooksRequest request,
            //Благодаря [AsParameters] система автоматически:
            //возьмёт PageNumber=2 из query-строки,
            //возьмёт PageSize=10 из query-строки,
            //создаст объект:
            //new GetBooksRequest(PageNumber: 2, PageSize: 10);
            //И передаст его в метод.
            //[AsParameters] — это удобный способ сразу получать объект-запрос из query-параметров, а не парсить их руками.

            ISender sender) =>
        //sender.Send(...) отправляет запрос на получение книг (GetBookQuery).
        //Код пойдёт к обработчику (GetBooksQueryHandler), который достанет список книг из базы данных.
        {
            GetBooksQuery query = request.Adapt<GetBooksQuery>();
            //request.Adapt<GetBooksQuery>() — это Mapster: он берёт request и создаёт новый объект GetBooksQuery, копируя совпадающие по имени свойства (PageNumber, PageSize).

            //То есть руками писать не нужно:
            //var query = new GetBooksQuery(request.PageNumber ?? 1, request.PageSize ?? 5);

            //Вместо этого Mapster всё делает за меня, потому что названия свойств совпадают.


            GetBooksResult result = await sender.Send(query);
            //query - только что создали объект GetBooksQuery (через request.Adapt<GetBooksQuery>()).Он описывает: «Дай мне книги с такой-то страницы и таким-то размером».

            //sender — это объект типа ISender (из библиотеки MediatR). Его задача — отправить запрос (query) «кому-то, кто умеет на него отвечать».

            //MediatR сам найдёт нужный Handler (например, GetBooksQueryHandler), потому что мы зарегистрировали его в DI.


            GetBooksResponse response = result.Adapt<GetBooksResponse>();
            //- Мы преобразуем результат (`GetBooksResult`) в `GetBooksResponse`.  
            //- Для преобразования используется библиотека **Mapster**, чтобы не писать вручную `new GetBooksResponse(result.Books)`.  

            return Results.Ok(response);
        });
    }
}
