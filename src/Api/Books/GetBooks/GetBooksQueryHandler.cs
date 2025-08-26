using System.Reflection.Metadata;
using Api.CQRS;
using Marten;
using Marten.Internal.Sessions;
using Api.Model;

namespace Api.Books.GetBooks; //пространство имён, где будет храниться логика получения книг.

public record GetBookQuery() : IQuery<GetBooksResult>; //GetBookQuery - получить список книг

public record GetBooksResult(IEnumerable<Book> Books); //то коробка-ответ.
//В ней хранится список (IEnumerable) книг (Book).


//Обработчик (Handler) – тот, кто реально ищет книги
public class GetBooksQueryHandler(IDocumentSession session) : IQueryHandel<GetBookQuery, GetBooksResult>
//У него есть доступ к базе (session) — это как полка с настоящими книгами.
//Он умеет брать бумажку-запрос GetBookQuery и давать коробку-ответ GetBooksResult.
{
    public async Task<GetBooksResult> Handle(GetBookQuery request, CancellationToken ct)
    //Метод Handle – «выполнить запрос»
    {
        var books = await session.Query<Book>().ToListAsync(ct);
        //session.Query<Book>() – библиотекарь идёт к полке и ищет все книги.
        //.ToListAsync(ct) – собирает их в список (коробку с книгами).
        //return new GetBooksResult(books) – отдаёт коробку тому, кто просил.
        return new GetBooksResult(books);
    }
}
