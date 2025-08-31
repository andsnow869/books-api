using System.Security.Cryptography;
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


        //ошибка автоматически превращается в JSON и уходит клиенту
        await httpContext.Response.WriteAsJsonAsync(
            problems,
            cancellationToken: cancellationToken
        );
        return true;

    }
}
