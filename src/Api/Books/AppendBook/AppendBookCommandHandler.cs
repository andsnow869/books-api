using Api.CQRS;
using Api.Model;
using Marten;

namespace Api.Books.AppendBook;

//добавить новую книгу в базу
//входящие данные
public record AppendBookCommand(
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
) : ICommand<AppendBookResult>;


//описывает, в каком формате отдавать результат
//Когда книга сохранена в базу, система возвращает Id книги, чтобы мы знали, какая именно книга добавлена.
public record AppendBookResult(Guid id);

//обработчик, который сохраняет книгу в БД
public class AppendBookCommandHandler(IDocumentSession session)
   : ICommandHandler<AppendBookCommand, AppendBookResult>
{
    public async Task<AppendBookResult> Handle(
        AppendBookCommand command, CancellationToken cancellationToken)
    //CancellationToken — штука, которая позволяет прервать выполнение (например, если сервер перегружен).
    {
        var book = new Book
        {
            Title = command.Title,
            Name = command.Name,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Price = command.Price,
            Category = command.Category
        };

        session.Store(book);
        await session.SaveChangesAsync(cancellationToken);

        return new AppendBookResult(book.Id); //ответ (Id новой книги).
    }

    //В Handle приходит команда (command) → в ней данные о книге.
    //Мы создаём новый объект Book и копируем туда данные из команды.
    //Через session.Store(book) готовим книгу к сохранению.
    //Через session.SaveChangesAsync() сохраняем в базе.
    //Возвращаем AppendBookResult, где кладём book.Id.
}

