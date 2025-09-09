using MediatR;

namespace Api.CQRS;

public interface ICommand : ICommand<Unit>
{ }

public interface ICommand<out TResponse> : IRequest<TResponse>
{ }

//📌 Заметки:
//ICommand – это маркерный интерфейс для команд, которые ничего не возвращают (например, "Создать пользователя", "Удалить заказ").
//ICommand<TResponse> – это интерфейс для команд, которые возвращают результат (TResponse).
//IRequest<TResponse> – это часть MediatR. Она говорит: "Это запрос/команда, которую обработает MediatR, и в ответ придёт объект типа TResponse".
//Unit – это специальный тип в MediatR, который означает "ничего не возвращает" (похож на void, но нужен для обобщённых интерфейсов).