
using System.Diagnostics;

namespace Api.Behaviors;

//это класс, который "перехватывает" запросы MediatR
//TRequest - сам запрос (например, )
//TResponse - ответ (например, )
public class TimeoutBehavior<TRequest, TResponse>
    (ILogger<TimeoutBehavior<TRequest, TResponse>> logger)
    //ILogger - чтобы писать сообщения в консоль или в файл

    : IPipelineBehavior<TRequest, TResponse>
    //IPipelineBehavior<TRequest, TResponse> - это как фильтр/прослойка между запросом и его обработкой.

    where TRequest : notnull, IRequest<TResponse>
    //ограничение: TRequest не должен быть пустым и должен быть запросом MediatR

    where TResponse : notnull
    //ограничение: TResponse - не должен быть пустым
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var timer = new Stopwatch(); //таймер
        timer.Start(); //запускаем отсчет времени
        var response = await next(); //запрос передается обработчику
        timer.Stop(); //останавливает секундомер

        //формируем строку с информацией: какой запрос и сколько мс он выполнялся
        string info = $" > Запрос: {typeof(TRequest).Name} выполняется {timer.Elapsed.TotalMilliseconds} ms";

        if (timer.Elapsed.Seconds > 2) // если прошло больше 2 секунд
        {
            logger.LogWarning(info); // пишем в терминал как предупреждение
        }
        else
        {
            logger.LogInformation(info); // иначе пишем как информацию
        }

        return response; //возвращаем результат дальше
    }
}
