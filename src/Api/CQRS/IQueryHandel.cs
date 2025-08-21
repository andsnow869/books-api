//это обработчик (как я получаю это)
using MediatR;

namespace Api.CQRS;

public interface IQueryHandel<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
//IQueryHandler<TQuery, TResponse> – это обработчик запроса.
//TQuery – это конкретный запрос (например, GetUsersQuery).
//TResponse – это то, что он вернёт (например, List<User>).
//: IRequestHandler<TQuery, TResponse> – говорит MediatR: "этот обработчик умеет обрабатывать такие запросы".

//where TQuery : IQuery<TResponse> → запрос обязан реализовать IQuery<TResponse>.
//where TResponse : notnull → результат не может быть null.

//Пример:
//👉 У нас есть запрос: GetUsersQuery : IQuery<List<User>>.
//👉 У нас есть обработчик: GetUsersHandler : IQueryHandler<GetUsersQuery, List<User>>.
//👉 Когда кто-то скажет: mediator.Send(new GetUsersQuery()), MediatR найдёт GetUsersHandler и вызовет его.
//👉 Обработчик достанет данные из базы и вернёт List<User>.
