using Api.CQRS;
using Api.Model;
using Mapster;
using Marten;

namespace Api.Books.UpdateBook;

//Это команда, где содержатся данные для обновления книги
//Перечислены все поля, которые можно поменять: Title, Name, Description, ImageUrl, Price, Category
//есть Id, чтобы найти нужную книгу в базе
public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
) : ICommand<UpdateBookResult>; //возвращает результат типа UpdateBookResult


//результат выполнения команды
//хранит только флаг IsSuccess (удалось обновить или нет)
public record UpdateBookResult(bool IsSuccess);


//обработчик команды
//обработчику поступает команда UpdateBookCommand и он возвращает результат UpdateBookResult
public class UpdateBookCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateBookCommand, UpdateBookResult>
{
    //метод Handle (метод запускается, когда кто-то вызывает команду)
    public async Task<UpdateBookResult> Handle(
        UpdateBookCommand command,
        CancellationToken cancellationToken)
    {
        //находим книгу в базе
        var book = await session.LoadAsync<Book>(command.Id, cancellationToken);
        //LoadAsync - ищет книгу по Id
        //если книги нет - возвращается null

        //проверяем, найдена ли книга
        if (book is null)
        {
            throw new Exception();
        }


        //ОБНОВЛЯЕМ ПОЛЯ КНИГИ

        //можно вручную переписать каждое поле:

        //book.Name = command.Name;
        //book.Title = command.Title;
        //book.Description = command.Description;
        //book.ImageUrl = command.ImageUrl;
        //book.Price = command.Price;
        //book.Category = command.Category;

        //но вместо ручного переписывания используется Mapster

        //Adapt копирует все свойства из команды command в объект book
        //то есть все новые значения переносятся сразу
        command.Adapt(book);

        //обновляем запись в базе
        //Update говорит Marten, что книга изменилась
        session.Update(book);

        //SaveChangesAsync сохраняет изменения в базе
        await session.SaveChangesAsync(cancellationToken);

        //возвращаем результат
        return new UpdateBookResult(true);
    }
}
