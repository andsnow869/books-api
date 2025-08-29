using Api.CQRS;
using FluentValidation;

namespace Api.Behaviors;

//ValidatorBehavior - проверяет все команды (command) перед тем, как они попадут в обработчик (файлы Handler - Handle - метод, который и делает всю работу, например сохраняет или создает книгу)

public class ValidationBehavior<TRequest, TResponse>
//TRequest - это тип запроса
//TResponse - это тип ответа
//то есть этот класс может работать с любыми типами запросов и ответов

    (IEnumerable<IValidator<TRequest>> validators)
    //IValidator<TRequest> - это правило проверки для определенного запроса
    //IEnumerable<IValidator<TRequest>> - это список всех правил


    : IPipelineBehavior<TRequest, TResponse>
    //реализует интерфейс pipeline behavior (перехватчик в конвейере)

    where TRequest : ICommand<TResponse>
    //Ограничение: TRequest - должен быть командой, возвращающей TResponse

{
    public async Task<TResponse> Handle(
        // метод, который вызывается при прохождении команды через pipeline (перехватчик)

        TRequest request,
        //request - сама команда (входные данные)

        RequestHandlerDelegate<TResponse> next,
        //next - функция, вызывающая следующий шаг (обычно Handler)

        CancellationToken cancellationToken)
    //токен отмены - чтобы прервать операцию при необходимости
    {
        var context = new ValidationContext<TRequest>(request);
        // var context = new ValidationContext<TRequest>(request) - эта строка подготавливает объект запроса так, чтобы валидаторы могли с ним работать

        //Например: у меня есть запрос AppendBookCommand (string Title, string Name, string Description...)
        //Когда MediatR вызывает ValidationBehavior, в него попадает такой объект:
        // var request = new AppendBookCommand("Война и мир", "Толстой", "Описание"...)

        //Чтобы валидаторы могли его проверить, нужно упаковать этот объект в специальный контекст
        // var request = new ValidateContext<AppendBookCommand>(request);


        var validationResult = await Task.WhenAll(
           validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );
        // запускаем все валидаторы и ждем завершения
        //validators - коллекция(список, массив и т.д.) валидаторов


        var failures = validationResult ////собираем все ошибка из результатов в один список
            .Where(r => r.Errors.Any()) //берем только те результаты, где есть ошибки
            .SelectMany(r => r.Errors)  // в один массив добавляет ошибки (например [Error1, Error2, Error3]), иначе было бы [Error1, Error2], [Error3],[]
            .ToList();                   //превращаем в List



        //если есть хоть одна ошибка, останавливаем выполнение и выбрасываем исключение (throw new Exception)
        if (failures.Any())
        {
            throw new Exception("Ошибка валидации");
        }

        //если ошибок нет, пропускаем команду дальше (вызываем следующий обработчик)
        return await next();
    }
}
