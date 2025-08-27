using System.Reflection.Metadata;
using Api.CQRS;
using Marten;
using Marten.Internal.Sessions;
using Api.Model;
using Marten.Pagination;

namespace Api.Books.GetBooks; //пространство имён, где будет храниться логика получения книг.

//В данном случае это "запрос" (Query), который мы отправим в систему через MediatR.
public record GetBooksQuery(int? PageNumber = 1, int? PageSize = 5) : IQuery<GetBooksResult>;
//GetBookQuery - по сути — объект-запрос: «Дай мне список книг с такой-то страницы и с таким-то количеством элементов».

//То есть, если не передать значения, по дефолту система возьмёт первую страницу и 5 элементов.

public record GetBooksResult(IEnumerable<Book> Books); //то коробка-ответ.
//В ней хранится список (IEnumerable) книг (Book).


//Обработчик (Handler) – тот, кто реально ищет книги
public class GetBooksQueryHandler(IDocumentSession session) : IQueryHandel<GetBooksQuery, GetBooksResult>
//У него есть доступ к базе (session) — это как полка с настоящими книгами.
//Он умеет брать бумажку-запрос GetBookQuery и давать коробку-ответ GetBooksResult.
{
    public async Task<GetBooksResult> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    //Метод Handle – «выполнить запрос»

    //async- Метод асинхронный.
    //Это значит, что он может выполнять действия, которые занимают время(например, запрос к базе данных), и не блокировать всю программу.
    //Асинхронные методы возвращают Task

    //GetBooksQuery query → сюда приходит сам запрос, который содержит данные (например, номер страницы и размер страницы).

    //CancellationToken cancellationToken → специальный токен, с помощью которого можно отменить выполнение задачи (например, если пользователь закрыл приложение, чтобы не тратить ресурсы).

    {
        var books = await session.Query<Book>()
        //.ToListAsync(cancellationToken);
        .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 5, cancellationToken);
        //session.Query<Book>() – библиотекарь идёт к полке и ищет все книги.
        //.ToListAsync(cancellationToken) – собирает их в список (коробку с книгами).
        //return new GetBooksResult(books) – отдаёт коробку тому, кто просил.
        return new GetBooksResult(books);
    }
}
