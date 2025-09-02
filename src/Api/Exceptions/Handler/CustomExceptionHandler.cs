using Api.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Exceptions.Handler;

//собственный обработчик ошибок
public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        //bool → результат (удалось обработать ошибку или нет).
        HttpContext httpContext,  //контекст запроса - все что связано с запросом: путь, заголовки, тело ответа
        Exception exception, //сама ошибка, которую нужно обработать
        CancellationToken cancellationToken)
    {
        //Тут ошибка записывается в логи (терминал, файл или специальный сервис для разработчиков - например Postman)с сообщением и временем, когда ошибка случилась
        logger.LogError(
            "Ошибки: {exceptionMessage}, время: {time}",
            exception.Message,  //сюда подставится текст ошибки
            DateTime.Now        //сюда подставится время
        );


        (string Detail, string Title, int StatusCode) details = exception switch
        //Detail - подробное описание (текст ошибки), Title - тип ошибки, StatusCode - HTTP статус (по умолчанию 500 Internal Server Error)
        {
            //обработка конкретного исключения (исключение, если нет id)
            BookNotFoundException =>
            (
              exception.Message,  //текст ошибки
              exception.GetType().Name, //тип ошибки
              httpContext.Response.StatusCode = StatusCodes.Status404NotFound
            ), //код ответа задаем HTTP = 404(ресурс не найден, книга не найдена)


            //общий перечень исключений
            _ => (
              exception.Message,  //текст ошибки
              exception.GetType().Name, //тип ошибки
              httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            )   //код ответа задаем HTTP = 500(Internal Server Error)
        };


        //ProblemDetails - это стандартный json - формат ошибок в ASP.NET Core
        //формирование ответа в формате ProblemDetails
        var problems = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path //пусть, в котором произошла ошибка, например,  "/api/books/5"
        };

        if (exception is FluentValidation.ValidationException validationException)
        //exception - объект ошибки, который где-то возник
        //is — проверяет, является ли этот объект именно FluentValidation.ValidationException
        //Если да, то создаётся новая переменная validationException, в которой хранится та же ошибка, но уже «приведённая» к правильному типу.
        {
            problems.Extensions.Add("Errors", validationException.Errors);
            //problems — это объект ProblemDetails (специальный стандартный ответ в ASP.NET для ошибок)
            //эта строка добавляет в ответ список всех ошибок проверки под ключом "Errors"
        }


        //ошибка автоматически превращается в JSON и уходит клиенту
        await httpContext.Response.WriteAsJsonAsync(
            problems,
            cancellationToken: cancellationToken
        );
        return true;

    }
}
