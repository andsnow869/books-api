//запрос (что я хочу получить)
namespace Api.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}

//IQuery = запрос (например, «дай список пользователей»).
//TResponse = тип ответа (например, List<UserDto>).
// : IRequest<TResponse> → означает, что MediatR понимает, что этот объект можно отправить через Mediator.Send() и он вернёт результат типа TResponse.
//where TResponse : notnull → ответ всегда должен существовать, не null.
