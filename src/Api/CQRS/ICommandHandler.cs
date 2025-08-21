//обработчики команд
using MediatR;

namespace Api.CQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}


public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{ }

//❗️ Заметки:
//ICommandHandler<in TCommand> - Это интерфейс для обработчика команды без результата.
//Наследуется от ICommandHandler<TCommand, Unit>.
//Unit = "ничего не возвращаем".
//Ограничение where TCommand : ICommand<Unit> → говорит, что этот обработчик принимает только те команды, которые не возвращают результат.

//ICommandHandler<in TCommand, TResponse> - Это интерфейс для обработчика команды с результатом.
//Наследуется от IRequestHandler<TCommand, TResponse> (часть MediatR).
//Ограничение where TCommand : ICommand<TResponse> → команда должна соответствовать типу ответа.
//👉 Пример: если команда CreateUserCommand возвращает Guid, то и обработчик должен быть ICommandHandler<CreateUserCommand, Guid>.
//where TResponse : notnull → результат обязательно должен быть (не null).

//❗️ Важно:
//Если команда ничего не возвращает → используем ICommandHandler<TCommand>.
//Если команда возвращает результат → используем ICommandHandler<TCommand, TResponse>.
